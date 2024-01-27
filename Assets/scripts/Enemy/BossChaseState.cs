using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossChaseState : BaseState
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
    public bool IsAttack => isAttak;
    public override void OnEnter(Enemy enemy)
    {
        currentEnemy = enemy;
        //获取attack脚本里的变量和方法
        attack = enemy.GetComponent<Attack>();
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
            //奔跑状态下 撞墙之后转身
            currentEnemy.transform.localScale = new Vector3(currentEnemy.faceDir.x, 0.5f, 0.5f);

        }
        //计时器
        attackRateCounter -= Time.deltaTime;
        //获取当前npc坐标的位置
        target = new Vector3(currentEnemy.attacker.position.x, 0, 0);
        Debug.Log("bbb");
        // Debug.Log("isattak");
        // Debug.Log(Mathf.Abs(target.x - currentEnemy.transform.position.x));
        //判断攻击距离(满足X轴的攻击距离)
        if (Mathf.Abs(target.x - currentEnemy.transform.position.x) <= attack.attackRange)
        {
            Debug.Log("aaa");
            //攻击
            isAttak = true;

            if (!currentEnemy.isHurt)
            {
                //boss停止移动
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

        } //超出攻击范围
        else
        {
            isAttak = false;
        }


        if (currentEnemy.anim.GetCurrentAnimatorStateInfo(0).IsName("BossAttack") || currentEnemy.anim.GetCurrentAnimatorStateInfo(0).IsName("BossAttack2"))
            return;
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

    }

    public override void OnExit()
    {
        //退出的时候跑步动画停止播放
        currentEnemy.anim.SetBool("run", false);
    }
}
