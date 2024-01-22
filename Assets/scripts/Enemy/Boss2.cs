using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Boss2 : Enemy
{

    [Header("移动范围")]
    public float patrolRadius;
    public float skillDistance;

    SpriteRenderer spriteRenderer;
    //重写 enemy中的awake方法，
    protected override void Awake()
    {
        //在enmey awake方法都执行的前提下
        base.Awake();
        patrolState = new Boss2PatrolState();
        chaseState = new Boss2ChaseState();
    }


    //蜜蜂重写FoundPlayer方法
    public override bool FoundPlayer()
    {
        //圆形检测器(位置为当前怪物的坐标，检测范围，检测对象)
        var obj = Physics2D.OverlapCircle(transform.position, checkDistance, attackLayer);
        //如果检测到人
        if (obj)
        {
            //把当前的位置赋值给攻击者
            attacker = obj.transform;
        }
        return obj;
    }
    // public override void OnDrawGizmosSelected()
    // {   //绘制检测范围（位置为当前怪物的坐标，检测范围）
    //     Gizmos.DrawWireSphere(transform.position, checkDistance);
    //     //检测移动范围颜色
    //     Gizmos.color = Color.green;
    //     //绘制移动范围
    //     Gizmos.DrawWireSphere(transform.position, patrolRadius);
    // }

    // //重写获得坐标的方法
    // public override Vector3 GetNewPoint()
    // {
    //     //随机获得X和Y的坐标值
    //     var targetX = Random.Range(-patrolRadius, patrolRadius);
    //     var targetY = Random.Range(-patrolRadius, patrolRadius);
    //     //在生成点基础上随机的一个范围点（范围点大小就是patrolRadius）
    //     return spwanPoint + new Vector3(targetX, targetY);
    // }

    public override void OnTakeDamage(Transform attackTrans)
    {
        //记录传参进来的攻击者
        attacker = attackTrans;
        //被攻击后转身(如果攻击我的人的x坐标减去我自身的x坐标大于0，就代表人在怪物右侧)
        if (attackTrans.position.x - transform.position.x > 0)
        {
            //被攻击后转身 直接写死为-1（因为当前时取的-1的值面朝左侧）
            transform.localScale = new Vector3(-5, 5, 5);
        }
        if (attackTrans.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(5, 5, 5);
        }
        //受伤后被击退 ishurt判断是否受伤
        isHurt = true;
        //受伤后执行受伤动画
        anim.SetTrigger("hurt");
        StartCoroutine(OnHurt());

    }

    //携程方法（可以按照等待执行）
    private IEnumerator OnHurt()
    {
        //等待0.5秒后执行下一帧
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }

    public override void Move()
    {
        // Debug.Log($"boss2 move{currentState is Boss2ChaseState}");
        if (currentState is Boss2ChaseState)
        {

            if (anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2") || anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                // Debug.Log($"boss2 move 222");
                return;
            }
        }

        //怪物刚体移动，y轴为默认值
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);

    }

    //死亡方法
    public override void OnDie()
    {
        //死亡的第一时间把碰撞体的涂层改为编号为2的层，然后在Edit -> project setting -> physis 2d -> layercollision matrix中修改碰撞图层
        gameObject.layer = 2;
        //执行死亡动画
        anim.SetBool("dead", true);
        //为true时死亡
        isDead = true;
    }


    public override void DestroyAfterAnimation()
    {
        base.DestroyAfterAnimation();
        //单例
        BossManager.Instance.Boss2Dead();
    }



}
