using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bee : Enemy
{
    [Header("移动范围")]
    public float patrolRadius;

    protected override void Awake()
    {
        base.Awake();
        //创建一个蜜蜂状态机脚本的实例
        patrolState = new BeePatrolState();
        chaseState = new BeeChaseState();
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
    public override void OnDrawGizmosSelected()
    {   //绘制检测范围（位置为当前怪物的坐标，检测范围）
        Gizmos.DrawWireSphere(transform.position, checkDistance);
        //检测移动范围颜色
        Gizmos.color = Color.green;
        //绘制移动范围
        Gizmos.DrawWireSphere(transform.position, patrolRadius);
    }

    //重写获得坐标的方法
    public override Vector3 GetNewPoint()
    {
        //随机获得X和Y的坐标值
        var targetX = Random.Range(-patrolRadius, patrolRadius);
        var targetY = Random.Range(-patrolRadius, patrolRadius);
        //在生成点基础上随机的一个范围点（范围点大小就是patrolRadius）
        return spwanPoint + new Vector3(targetX, targetY);
    }

    public override void Move()
    {

    }
}
