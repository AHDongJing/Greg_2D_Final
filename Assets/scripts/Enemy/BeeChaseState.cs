using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeChaseState : BaseState
{
    private Attack attack;
    //目标位置
    private Vector3 target;
    //移动方向
    private Vector3 moveDir;
    //是否处于攻击状态
    private bool isAttak;

    //攻击计时器
    private float attackRateCounter = 0;

    public override void OnEnter(Enemy enemy)
    {
        //currentEnemy等于当前传参进来的enemy
        currentEnemy = enemy;
        //进入到巡逻状态的时候当前移动速度变为走路的速度
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        //获取attack脚本里的变量和方法
        attack = enemy.GetComponent<Attack>();
        //进入状态时重制以下计时器时间（解决蜜蜂追到人后疯狂左右转动）
        currentEnemy.lostTimeCounter = currentEnemy.lostTime;

        //状态开始时就播放追击动画
        currentEnemy.anim.SetBool("chase", true);
    }

    public override void LogicUpdate()
    {
        //如果超出了丢失玩家的时间
        if (currentEnemy.lostTimeCounter <= 0)
        {
            //从追击切换到巡逻状态
            currentEnemy.SwitchState(NPCState.Patrol);
        }
        //计时器
        attackRateCounter -= Time.deltaTime;
        //获取当前npc坐标的位置 Y轴+1.5f因为蜜蜂的中心点在身子中间，但是player的中心点在脚底，所以需要增加高度避免蜜蜂飞到player脚下
        target = new Vector3(currentEnemy.attacker.position.x, currentEnemy.attacker.position.y + 1.5f, 0);

        //判断攻击距离(满足X和Y轴的攻击距离)
        if (Mathf.Abs(target.x - currentEnemy.transform.position.x) <= attack.attackRange && Mathf.Abs(target.y - currentEnemy.transform.position.y) <= attack.attackRange)
        {
            //攻击
            isAttak = true;
            if (!currentEnemy.isHurt)
            {
                //蜜蜂停止移动
                currentEnemy.rb.velocity = Vector2.zero;

            }
            //如果攻击时间小于0
            if (attackRateCounter <= 0)
            {
                //播放攻击动画
                currentEnemy.anim.SetTrigger("attack");
                //重制攻击时间
                attackRateCounter = attack.attackRate;

            }

        }
        //超出攻击范围
        else
        {
            isAttak = false;
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
        //不处于受伤，死亡，攻击状态时
        if (!currentEnemy.isHurt && !currentEnemy.isDead && !isAttak)
        {
            //给刚体施加力来进行移动 = 移动方向*当前移动速度*时间修正
            currentEnemy.rb.velocity = moveDir * currentEnemy.currentSpeed * Time.deltaTime;
        }

    }
    public override void OnExit()
    {

        //状态结束后就播放追击动画
        currentEnemy.anim.SetBool("chase", false);
    }
}
