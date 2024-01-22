using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boss2ChaseState : BaseState
{
    private Attack attack;
    //目标位置
    private Vector3 target;
    //移动方向
    private Vector3 moveDir;
    //是否处于攻击状态
    private bool isAttack;

    //攻击计时器
    private float attackRateCounter = 0;
    private Boss2 boss2;

    LayerMask checkLayer = 1 << 7;
    public override void OnEnter(Enemy enemy)
    {
        //currentEnemy等于当前传参进来的enemy
        currentEnemy = enemy;
        //进入到巡逻状态的时候当前移动速度变为走路的速度
        currentEnemy.currentSpeed = currentEnemy.chaseSpeed;
        //获取attack脚本里的变量和方法
        attack = enemy.GetComponent<Attack>();
        boss2 = enemy.GetComponent<Boss2>();

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
        //获取当前npc坐标的位置 
        target = new Vector3(currentEnemy.attacker.position.x, currentEnemy.attacker.position.y, 0);

        //判断攻击距离(满足X和Y轴的攻击距离)
        // RaycastHit2D[] raycastHit2D;
        // 检测地板，为了应对动画无法从任意高度直接降落到地板上，所以限制了高度区间
        RaycastHit2D checkGround = Physics2D.Raycast(currentEnemy.transform.position + Vector3.up * 3, Vector2.down, boss2.skillDistance, checkLayer);
        // Vector3 start = currentEnemy.transform.position + Vector3.up * 3;
        // Vector3 end = new Vector3(start.x, start.y - boss2.skillDistance);
        // Debug.DrawLine(start, end, Color.red);
        // Debug.Log($"raycastHit2D={raycastHit2D},Mathf.Abs(target.x - currentEnemy.transform.position.x)={Mathf.Abs(target.x - currentEnemy.transform.position.x)},attack.attackRange={attack.attackRange}");

        if (checkGround && Mathf.Abs(target.x - currentEnemy.transform.position.x) <= attack.attackRange)
        {

            // 如果玩家在boss脚底，则会重复满足条件，因为攻击动画是trigger，所以会重复开始放攻击动画， 这里判断如果是正在播放则只改状态， 不进行触发
            if (currentEnemy.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2") || currentEnemy.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                isAttack = true;
                // return;
            }
            else
            {
                //攻击
                isAttack = true;
                if (!currentEnemy.isHurt)
                {
                    //boss2停止移动
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
        }
        //超出攻击范围
        else
        {
            isAttack = false;
            if (currentEnemy.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack2") || currentEnemy.anim.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
            {
                isAttack = true;
                // return;
            }
        }

        //移动方向等于目标方向减去自己当前的坐标
        moveDir = (target - currentEnemy.transform.position).normalized;
        //移动时改变面朝方向
        if (moveDir.x > 0)
        {
            currentEnemy.transform.localScale = new Vector3(-5f, 5f, 5f);
        }

        if (moveDir.x < 0)
        {
            currentEnemy.transform.localScale = new Vector3(5f, 5f, 5f);
        }
    }


    public override void PhysicsUpdate()
    {
        // Debug.Log($"isAttak={isAttack},isHurt={currentEnemy.isHurt},isDead={currentEnemy.isDead}");
        // Debug.Log("aaa:" + moveDir * currentEnemy.currentSpeed * Time.deltaTime);
        //不处于受伤，死亡，攻击状态时
        if (!currentEnemy.isHurt && !currentEnemy.isDead && !isAttack)
        {
            //给刚体施加力来进行移动 = 移动方向*当前移动速度*时间修正
            currentEnemy.rb.velocity = moveDir * currentEnemy.currentSpeed * Time.deltaTime;
        }

    }
    public override void OnExit()
    {

        //状态结束后就停止追击动画
        currentEnemy.anim.SetBool("chase", false);
    }
}
