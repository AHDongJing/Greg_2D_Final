using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //获取刚体组件
    private Rigidbody2D rb;

    //获取碰撞体组件
    private CapsuleCollider2D coll;

    //通过这个变量调用PlayerinputControl所设置的变量,
    public PlayerinputControl inputcontrol;
    //通过这个变量调用Character所设置的变量,
    public Character character;


    //访问物理检测脚本
    private PhisycsCheck phisycsCheck;

    //访问动画脚本（PlayerAnimation）
    private PlayerAnimation playerAnimation;
    //通过inputDirection变量读取输入的值
    public Vector2 inputDirection;
    [Header("基本参数")]

    //跳跃次数限制 2
    public int jumpAmountMax = 2;
    //跳跃计数器
    public int jumpAmount;
    [Header("移动速度")]
    public float speed;
    [Header("跑动速度")]
    private float runSpeed;
    //初始移动速度的一半(用于切换为走路姿势)
    private float walkSpeed => speed / 2.5f;
    [Header("跳跃力")]
    public float jumpForce;

    [Header("蹬墙跳跃力")]
    public float wallJumpForce;

    [Header("滑铲距离")]
    public float slideDistance;
    [Header("滑铲速度")]
    public float slideSpeed;

    [Header("滑铲能量消耗")]
    public int slidePowerCost;

    [Header("下蹲状态")]
    public bool isCrouch;

    //记录原始碰撞体的位置（位移）
    private Vector2 originalOffset;
    //记录原始碰撞体的尺寸
    private Vector2 originalSize;

    [Header("是否受伤")]
    public bool isHurt;
    [Header("受伤后的反弹力度")]
    public float hurtForce;
    [Header("是否死亡")]
    public bool isDead;
    [Header("是否攻击")]
    public bool isAttack;
    [Header("是否在墙上跳跃")]
    public bool wallJump;

    [Header("是否滑铲")]
    public bool isSlide;

    [Header("物理材质")]
    public PhysicsMaterial2D wall;
    public PhysicsMaterial2D normal;


    //Awale最先运行的方法
    private void Awake()
    {
        //获取刚体组件
        rb = GetComponent<Rigidbody2D>();
        //获取碰撞体组件
        coll = GetComponent<CapsuleCollider2D>();
        //拿到碰撞体offset的初始值（碰撞体的位置）
        originalOffset = coll.offset;
        //拿到碰撞体size初始值（碰撞体的大小）
        originalSize = coll.size;

        //获得物理检测脚本(PhisycsCheck)中设置好的所有变量
        phisycsCheck = GetComponent<PhisycsCheck>();
        //获得动画脚本(PlayerAnimation)中设置好的所有变量
        playerAnimation = GetComponent<PlayerAnimation>();
        //获得character脚本中设置好的所有变量
        character = GetComponent<Character>();

        inputcontrol = new PlayerinputControl();



        //读取Gameplayer中的Jump属性每次按下时执行（单次执行started）
        inputcontrol.Gameplayer.Jump.started += Jump;
        #region 强制走路
        //游戏开始的时候获取speed速度
        runSpeed = speed;
        //按下（performed）左上挡键位切换为走路姿势
        inputcontrol.Gameplayer.WalkButton.performed += ctx =>
        {
            if (phisycsCheck.isGround)
            {
                speed = walkSpeed;
            }
        };
        //松开（canceled）左上挡键位切换为走路姿势
        inputcontrol.Gameplayer.WalkButton.canceled += ctx =>
        {
            if (phisycsCheck.isGround)
            {
                speed = runSpeed;
            }
        };
        #endregion

        //攻击操作
        inputcontrol.Gameplayer.Attack.started += PlayerAttack;

        //滑铲
        inputcontrol.Gameplayer.Slide.started += Slide;
    }



    //当前物体被启动时，inputcontrol也启动 （物体启动 inspector中勾选物体）
    private void OnEnable()
    {
        inputcontrol.Enable();
    }
    //当物体被关闭时，inputcontrol也关闭 （物体启动 inspector中取消勾选物体）
    private void OnDisable()
    {
        inputcontrol.Disable();
    }
    private void Update()
    {

        //通过inputDirection变量读取输入Gameplayer中move的value值类型是vector2
        inputDirection = inputcontrol.Gameplayer.Move.ReadValue<Vector2>();

        //检查刚体使用什么材质方法
        CheckState();
    }

    //FixedUpdate修改物理判断的更新模式
    private void FixedUpdate()
    {
        //isHurt不为true并且非攻击状态下的时候才可以执行移动
        if (!isHurt && !isAttack && !isSlide)
        {
            Move();
        }

    }
    //移动方法
    public void Move()
    {
        //人物不处于下蹲时才可以移动，isCrouch为false时执行移动操作,同时不处于蹬墙跳时才能施加移动的力
        if (!isCrouch && !wallJump)
        {
            Debug.Log("aaa");
            //velocity改变刚体的线性速度通过vector2向量(传参为输入的移动值inputDirection)，创建人物输入方向和速度给到人物的刚体用来控制移动
            rb.velocity = new Vector2(inputDirection.x * speed * Time.deltaTime, rb.velocity.y/*维持原有的Y轴重力-9.81*/);
        }

        //人物翻转
        float faceDir = transform.localScale.x;
        if (inputDirection.x > 0)
        {
            //更改scale
            faceDir = 0.5f;
        }
        else if (inputDirection.x < 0)
        {
            faceDir = -0.5f;
        }
        transform.localScale = new Vector3(faceDir, 0.5f, 0.5f);

        //人物下蹲,当人物的y轴为负数的时候（按S的时候Y轴为负数）并且人物没有离地，isGround为true的时候，isCrouch为true；
        isCrouch = inputDirection.y < -0.5f && phisycsCheck.isGround;

        if (isCrouch || isSlide)
        {
            //isCrouch为true时人物下蹲时新建一个offset和size的值让碰撞体跟随缩小
            coll.offset = new Vector2(-0.05f, 0.85f);
            coll.size = new Vector2(0.7f, 1.7f);
        }
        else
        {
            //isCrouch为false时人物起立时还原Awake中的offset和size值
            coll.offset = originalOffset;
            coll.size = originalSize;
        }
    }


    //跳跃方法
    private void Jump(InputAction.CallbackContext context)
    {
        //判断是否接触到地面，IsGround为true才可以进行跳跃
        if (phisycsCheck.isGround)
        {
            jumpAmount = 0;
            jumpAmount++;
            //跳跃给刚体一个向上（transform.up）的力(jumpforce)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);

            //打断滑铲协程方法
            isSlide = false;
            StopAllCoroutines();
        }
        //判断是否在墙上
        else if (phisycsCheck.onWall)
        {
            jumpAmount = 0;
            jumpAmount++;
            //在墙上跳跃的时候给面朝的方向施加一个力，X轴为面冲的方向y轴给一个左右上角的力
            rb.AddForce(new Vector2(-inputDirection.x, 2f) * wallJumpForce, ForceMode2D.Impulse);
            //蹬墙跳时为true;
            wallJump = true;
        }
        //二段跳判断
        else if (!phisycsCheck.isGround)
        {
            if (jumpAmount >= jumpAmountMax)
            {
                return;
            }
            jumpAmount++;
            rb.velocity = Vector2.zero;
            //跳跃给刚体一个向上（transform.up）的力(jumpforce)
            rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
            //打断滑铲协程方法
            isSlide = false;
            StopAllCoroutines();
        }

    }

    //攻击方法
    private void PlayerAttack(InputAction.CallbackContext obj)
    {
        //如果人物不在地面上，就直接return不执行攻击动作
        if (!phisycsCheck.isGround)
        {
            return;
        }
        //播放攻击动画
        playerAnimation.PlayAttack();
        //进入攻击状态
        isAttack = true;

    }

    //滑铲方法
    private void Slide(InputAction.CallbackContext obj)
    {
        //处于非滑铲状态时并且在地面上使用滑铲必须当前能量大于使用消耗能量
        if (!isSlide && phisycsCheck.isGround && character.currentPower >= slidePowerCost)
        {
            //进入滑铲状态
            isSlide = true;

            //滑铲的目标点
            var targetPos = new Vector3(transform.position.x + slideDistance * transform.localScale.x, transform.position.y);

            //滑铲时把层级切换到Enemy 用来规避伤害
            gameObject.layer = LayerMask.NameToLayer("Enemy");
            //打开协程
            StartCoroutine(TriggerSlide(targetPos));
            //调用滑铲能量方法
            character.OnSlide(slidePowerCost);
        }
    }

    //滑铲协程方法，需要把targetPos传递进来
    private IEnumerator TriggerSlide(Vector3 target)
    {
        do
        {
            yield return null;
            //判断如果不在地面就停止
            if (!phisycsCheck.isGround)
            {
                break;
            }

            //判断如果撞墙就停止
            if (phisycsCheck.touchLeftWall && transform.localScale.x < 0f || phisycsCheck.touchRightWall && transform.localScale.x > 0f)
            {
                isSlide = false;
                break;
            }
            //滑铲前进（当前物体的方向和面朝方向*滑铲速度）
            rb.MovePosition(new Vector2(transform.position.x + transform.localScale.x * slideSpeed, transform.position.y));
        }
        while (MathF.Abs(target.x - transform.position.x) > 0.1f);
        isSlide = false;
        //滑铲结束后把层级切换回Player
        gameObject.layer = LayerMask.NameToLayer("Player");
    }


    //检查刚体材质方法
    private void CheckState()
    {
        //刚体中的Material使用什么材质，取决于是否在地上(isGround判断)，如果在地上就使用normal（带有摩擦力）否则使用wall
        coll.sharedMaterial = phisycsCheck.isGround ? normal : wall;
        //当处于墙壁的状态下
        if (phisycsCheck.onWall)
        {
            //y轴/2延缓下降速度
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y / 2f);
        }
        else
        {
            //离开墙壁的时候恢复速度
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
        }
        //当Y轴小于0时 walljump为false，解决了蹬墙跳落地后无法左右移动的问题（因为walljump落地后始终为true）
        if (wallJump && rb.velocity.y < 0f)
        {
            wallJump = false;
        }
        if (isDead || isSlide)
            gameObject.layer = LayerMask.NameToLayer("Enemy");
        else
            gameObject.layer = LayerMask.NameToLayer("Player");
    }

    #region unity事件执行方法
    //受到伤害后反弹方法
    public void GetHurt(Transform attacker)
    {
        //受到伤害时isHurt为true，执行下面的代码
        isHurt = true;
        //受到伤害后让刚体的移动速度0（vector2.zero）
        rb.velocity = Vector2.zero;
        //推动的方向是人物自己位置的x轴减去怪物位置的x轴，y轴为0（normalized为归一化，让这个数值在1以内防止过大）
        Vector2 dir = new Vector2((transform.position.x - attacker.position.x), 0).normalized;
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }

    //死亡处理方法
    public void PlayerDead()
    {
        //isDead为true表示人物死亡，死亡之后禁止所有的gameplayer的操作
        isDead = true;
        inputcontrol.Gameplayer.Disable();
    }
    #endregion
}
