using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Boss : Enemy
{

    //重写 enemy中的awake方法，
    protected override void Awake()
    {
        //在enmey awake方法都执行的前提下
        base.Awake();
        //创建一个boss的状态机脚本的实例
        patrolState = new BossPatrolState();
        chaseState = new BossChaseState();


        // //获得面朝方向通过transfrom.localScale.x的值 负数面朝右边正数面朝左边
        // faceDir = new Vector3(transform.localScale.x, 0, 0);
    }
    //重写FoundPlayer方法
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
    //死亡方法
    public override void OnDie()
    {
        if (isDead)
        {
            return;
        }
        //死亡的第一时间把碰撞体的涂层改为编号为2的层，然后在Edit -> project setting -> physis 2d -> layercollision matrix中修改碰撞图层
        gameObject.layer = 2;
        //执行死亡动画
        anim.SetBool("dead", true);
        currentState.OnExit();
        //为true时死亡
        isDead = true;
    }

    public override void Move()
    {
        if (currentState is BossChaseState)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).IsName("BossAttack") || anim.GetCurrentAnimatorStateInfo(0).IsName("BossAttack2"))
                return;
        }

        //怪物刚体移动，y轴为默认值
        rb.velocity = new Vector2(currentSpeed * faceDir.x * Time.deltaTime, rb.velocity.y);

    }

    public override void DestroyAfterAnimation()
    {
        base.DestroyAfterAnimation();
        //单例
        BossManager.Instance.Boss1Dead();
    }

}
