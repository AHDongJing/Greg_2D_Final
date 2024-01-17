using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoarChaseState : BaseState
{


    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        //Debug.Log("进入追击状态");
        //进入追击状态后，当前移动速度变为追击移动速度
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        //播放奔跑动画
        currentEnemy.anim.SetBool("run", true);
    }
    public override void LogicUpdate()
    {
        //如果超出了丢失玩家的时间
        if (currentEnemy.lostTimeCounter <= 0)
        {
            //从追击切换到巡逻状态
            currentEnemy.SwitchState(NPCState.Patrol);
        }
        //敌人检测如果不是在地面上或者碰触到左边或者碰触到右边墙壁（碰撞体）
        if (!currentEnemy.phisycsCheck.isGround || (currentEnemy.phisycsCheck.touchLeftWall && currentEnemy.faceDir.x < 0) || (currentEnemy.phisycsCheck.touchRightWall && currentEnemy.faceDir.x > 0))
        {
            //奔跑状态下 撞墙之间转身
            currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDir.x, 0.5f, 0.5f);

        }
    }


    public override void PhysicsUpdate()
    {

    }

    public override void OnExit()
    {
        //退出的时候跑步动画停止播放
        currentEnemy.anim.SetBool("run", false);
    }
}
