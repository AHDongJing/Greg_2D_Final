using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//通过抽象类继承而来的野猪巡逻方法
public class BoarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        //currentEnemy等于当前传参进来的enemy
        currentEnemy = enemy;
        //进入到巡逻状态的时候当前移动速度变为走路的速度
        currentEnemy.currentSpeed = currentEnemy.normalSpeed;
    }
    public override void LogicUpdate()
    {
        //检测前方是否有player,一旦找到后就把状态进行切换
        if (currentEnemy.FoundPlayer())
        {
            //状态切换到chase追击
            currentEnemy.SwitchState(NPCState.Chase);
        }

        //敌人检测如果不是在地面上或者碰触到左边或者碰触到右边墙壁（碰撞体）
        if (!currentEnemy.phisycsCheck.isGround || (currentEnemy.phisycsCheck.touchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.phisycsCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            //计时器启动
            currentEnemy.wait = true;
            currentEnemy.anim.SetBool("walk", false);

        }
        else
        {
            //播放行走动画
            currentEnemy.anim.SetBool("walk", true);
        }
    }


    public override void PhysicsUpdate()
    {

    }

    public override void OnExit()
    {
        //如果又发现了玩家，就重制计时器
        currentEnemy.lostTimeCounter = currentEnemy.lostTime;
        //退出的时候走路动画停止播放
        currentEnemy.anim.SetBool("walk", false);
    }
}
