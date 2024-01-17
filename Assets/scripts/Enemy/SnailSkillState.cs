using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailSkillState : BaseState
{

    public override void OnEnter(Enemy enemy)
    {
        //currentEnemy等于当前传参进来的enemy
        currentEnemy = enemy;
        //进入到巡逻状态的时候当前移动速度变为走路的速度
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        //walk为false不能移动
        currentEnemy.anim.SetBool("walk", false);
        //skill触发让蜗牛缩起来，然后hide为true保持缩起来的idle状态
        currentEnemy.anim.SetBool("hide", true);
        currentEnemy.anim.SetTrigger("skill");

        //进入技能状态时重制以下计时器时间（解决蜗牛缩壳后计时器依然非0导致蜗牛在缩壳状态时移动）
        currentEnemy.lostTimeCounter = currentEnemy.lostTime;

        //状态脚本执行时就变为无敌
        currentEnemy.GetComponent<Character>().invulnerable = true;
        //让无敌时间等于缩壳时间
        currentEnemy.GetComponent<Character>().invulnerableCounter = currentEnemy.lostTimeCounter;

    }
    public override void LogicUpdate()
    {
        //如果超出了丢失玩家的时间
        if (currentEnemy.lostTimeCounter <= 0)
        {
            //从追击切换到巡逻状态
            currentEnemy.SwitchState(NPCState.Patrol);
        }
        //让无敌时间等于缩壳时间
        currentEnemy.GetComponent<Character>().invulnerableCounter = currentEnemy.lostTimeCounter;
    }

    public override void PhysicsUpdate()
    {

    }
    public override void OnExit()
    {
        //退出时把躲藏状态变为false，true为躲藏/false为不躲藏
        currentEnemy.anim.SetBool("hide", false);

        //状态退出时无敌变为false
        currentEnemy.GetComponent<Character>().invulnerable = false;
    }

}
