using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

//被挂载的对象必须要有以下三个组件
[RequireComponent(typeof(Rigidbody2D), typeof(Animator), typeof(PhisycsCheck))]
public class Enemy : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    //只允许内部和子类访问（改为public可以让状态机进行访问）
    [HideInInspector] public Animator anim;
    //获取PhisycsCheck脚本组件
    [HideInInspector] public PhisycsCheck phisycsCheck;
    [Header("走路速度")]
    public float normalSpeed;
    [Header("追击速度")]
    public float chaseSpeed;

    [Header("当前移动速度")]
    [HideInInspector] public float currentSpeed;

    [Header("面朝方向")]
    public Vector3 faceDir;

    [Header("攻击者")]
    public Transform attacker;

    [Header("受伤后的推力")]
    public float hurtForce;

    [Header("蜜蜂出生点")]
    public Vector3 spwanPoint;

    [Header("受伤状态")]
    public bool isHurt;

    [Header("死亡状态")]
    public bool isDead;

    [Header("检测偏移")]
    public Vector2 centerOffset;
    [Header("检测盒子的大小")]
    public Vector2 checkSize;
    [Header("检测距离")]

    public float checkDistance;
    [Header("检测攻击者的图层")]

    public LayerMask attackLayer;


    [Header("计时器")]
    //等待时间
    public float waitTime;
    //等待计时器
    public float waitTimeCounter;
    //是否进入等待
    public bool wait;

    //丢失敌人后等待时间
    public float lostTime;
    //计数器
    public float lostTimeCounter;


    //当前状态
    protected BaseState currentState;

    //创建一个baseState的类型（巡逻状态），来调用重写的方法
    protected BaseState patrolState;
    //创建一个BorChaseState的类型（巡逻状态），来调用重写的方法
    protected BaseState chaseState;

    protected BaseState skillState;

    public BaseState GetCurrentState()
    {
        return currentState;
    }
    // //追击状态（抽象类重写）
    // protected BaseState cheseState;

    protected virtual void Awake()
    {
        //获取组件
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        phisycsCheck = GetComponent<PhisycsCheck>();
        //当前移动速度等于走路速度
        currentSpeed = normalSpeed;

        //waitTimeCounter = waitTime;

        //蜜蜂出生点(游戏运行时摆放的位置就是蜜蜂的出生点)
        spwanPoint = transform.position;

    }

    //周期状态（物体被激活时加载）
    private void OnEnable()
    {
        //游戏开始时怪物进入巡逻状态
        currentState = patrolState;
        //执行巡逻一开始的函数方法并传递一个当前的enmey(把自己传递进去方便抽象类调用)
        currentState?.OnEnter(this);
    }
    private void Update()
    {

        //获得面朝方向通过transfrom.localScale.x的值 负数面朝右边正数面朝左边
        faceDir = new Vector3(-transform.localScale.x, 0, 0);
        //当前状态下持续执行逻辑循环判断
        currentState?.LogicUpdate();
        //调用计时器
        TimeCounter();
        HandlerDir();
    }
    public virtual void HandlerDir()
    {

    }

    private void FixedUpdate()
    {
        //没有受伤或死亡或等待的时候才能执行移动方法
        if (!isHurt && !isDead && !wait)
        {
            //刚体移动
            Move();
        }
        //执行物理逻辑方法的内容
        currentState?.PhysicsUpdate();
    }

    //物体消失时执行一次的方法
    private void OnDisable()
    {
        //执行退出方法
        currentState.OnExit();
    }
    //怪物移动方法可被子类重写
    public virtual void Move()
    {
        //如果当前动画没有在播放蜗牛的PerMove就让怪物移动（不会影响其他怪物）
        //这里判断使用了动画组件的GetCurrentAnimatorStateInfo(),参数为Animator中的layers从0开始获取到layers后通过is.name找到具体的动画
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("snailPreMove") && !anim.GetCurrentAnimatorStateInfo(0).IsName("snailRecover"))
        {
            //怪物刚体移动，y轴为默认值
            rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);
        }

    }

    //检测玩家方法
    public virtual bool FoundPlayer()
    {
        //返回一个true和false，BoxCast用来向物体前方发射一个盒子检测体（位置时自身的位置+设置的偏移距离，盒子的大小，盒子的角度默认为0，盒子的方向为faceDir怪物的正前方，盒子的最大投射距离，检测的层）
        return Physics2D.BoxCast(transform.position + (Vector3)centerOffset, checkSize, 0, faceDir, checkDistance, attackLayer);
    }
    //切换状态方法(方法参数为枚举)
    public void SwitchState(NPCState state)
    {
        var newState = state switch
        {
            //如果判断为巡逻类型，就把状态切换到巡逻
            NPCState.Patrol => patrolState,
            //如果判断为追逐，就把状态切换到追逐
            NPCState.Chase => chaseState,
            //如果判断为使用技能状态，就把状态切换到技能
            NPCState.Skill => skillState,
            _ => null
        };
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);



    }
    //获得新坐标(仅蜜蜂使用)
    public virtual Vector3 GetNewPoint()
    {
        return transform.position;
    }

    //计时器方法
    public void TimeCounter()
    {
        //进入等待
        if (wait)
        {
            waitTimeCounter -= Time.deltaTime;
            //等待计时器为0时;
            if (waitTimeCounter <= 0)
            {
                //等待结束
                wait = false;
                //计时器时间修正
                waitTimeCounter = waitTime;
                //就进行反转改变transform.localScale.x的值 -1是面朝右边 正数是面朝左边
                transform.localScale = new Vector3(-transform.localScale.x, 0.5f, 0.5f);


            }
        }
        //如果没有检测到敌人
        if (!FoundPlayer() && lostTimeCounter > 0)
        {
            lostTimeCounter -= Time.deltaTime;
        }
    }

    //接受攻击
    public virtual void OnTakeDamage(Transform attackTrans)
    {
        //记录传参进来的攻击者
        attacker = attackTrans;
        //被攻击后转身(如果攻击我的人的x坐标减去我自身的x坐标大于0，就代表人在怪物右侧)
        if (attackTrans.position.x - transform.position.x > 0)
        {
            //被攻击后转身 直接写死为-1（因为当前时取的-1的值面朝左侧）
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
        if (attackTrans.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        //受伤后被击退 ishurt判断是否受伤
        isHurt = true;
        //受伤后执行受伤动画
        anim.SetTrigger("hurt");
        //dir记录攻击方向，（我当前的坐标减去攻击者的坐标得到攻击方向）
        Vector2 dir = new Vector2(transform.position.x - attackTrans.position.x, 0).normalized;
        //受伤时先让velocity停止下来,然后在执行Addforce的击退力
        rb.velocity = new Vector2(0, rb.velocity.y);

        //启动携程方法(固定写法)
        StartCoroutine(OnHurt(dir));


    }
    //携程方法（可以按照等待执行）
    private IEnumerator OnHurt(Vector2 dir)
    {
        //使用刚体的addforce添加一个dir方向的impulse冲击力  hurtforce为冲击力值在unity中添加
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);

        //等待0.5秒后执行下一帧
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }

    //死亡方法
    public virtual void OnDie()
    {
        if (isDead)
        {
            return;
        }
        //死亡的第一时间把碰撞体的涂层改为编号为2的层，然后在Edit -> project setting -> physis 2d -> layercollision matrix中修改碰撞图层
        gameObject.layer = 2;
        //执行死亡动画
        anim.SetBool("dead", true);
        //为true时死亡
        isDead = true;

    }

    //动画结束后销毁怪物
    public virtual void DestroyAfterAnimation()
    {
        //死亡后直接销毁自身
        Destroy(this.gameObject);
    }

    //绘制检测范围
    public virtual void OnDrawGizmosSelected()
    {
        //绘制一个球形的检测范围
        Gizmos.DrawWireSphere(transform.position + (Vector3)centerOffset + new Vector3(checkDistance * -transform.localScale.x, 0), 0.2f);
    }

}
