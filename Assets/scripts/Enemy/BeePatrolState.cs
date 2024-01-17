using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeePatrolState : BaseState
{
    //目标位置
    private Vector3 target;
    //移动方向
    private Vector3 moveDir;

    public override void OnEnter(Enemy enemy)
    {
        //currentEnemy等于当前传参进来的enemy
        currentEnemy = enemy;
        //进入到巡逻状态的时候当前移动速度变为走路的速度
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
        //回到生成点周围随机范围的一个点
        target = enemy.GetNewPoint();
    }
    public override void LogicUpdate()
    {
        //检测前方是否有player,一旦找到后就把状态进行切换
        if (currentEnemy.FoundPlayer())
        {
            //状态切换到追击
            currentEnemy.SwitchState(NPCState.Chase);
        }
        //判断是否已经到达指定坐标位置，使用绝对值来判断
        if (Math.Abs(target.x - currentEnemy.transform.position.x) < 0.1f && Math.Abs(target.y - currentEnemy.transform.position.y) < 0.1f)
        {
            //进入等待时间
            currentEnemy.wait = true;
            //重新获得坐标点
            target = currentEnemy.GetNewPoint();
        }
        //移动方向等于目标方向减去自己当前的坐标
        moveDir = (target - currentEnemy.transform.position).normalized;
        //移动时改变面朝方向
        if (moveDir.x > 0)
        {
            currentEnemy.transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }

        if (moveDir.x < 0)
        {
            currentEnemy.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
    }

    public override void PhysicsUpdate()
    {
        //不处于受伤，死亡，等待状态时
        if (!currentEnemy.isHurt && !currentEnemy.isDead && !currentEnemy.wait)
        {
            //给刚体施加力来进行移动 = 移动方向*当前移动速度*时间修正
            currentEnemy.rb.velocity = moveDir * currentEnemy.currentSpeed * Time.deltaTime;
        }
        else
        {
            currentEnemy.rb.velocity = Vector2.zero;
        }
    }
    public override void OnExit()
    {

    }
}
