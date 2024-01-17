using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [Header("最大血量")]
    public float maxHealth;
    [Header("当前血量")]
    public float currentHealth;

    [Header("最大滑铲能量")]
    public float maxPower;
    [Header("当前滑铲能量")]
    public float currentPower;
    [Header("滑铲能量恢复速度")]
    public float powerRecoverSpeed;

    [Header("受伤无敌时间")]
    public float invulnerableDurion;
    //受伤无敌的时间
    [HideInInspector] public float invulnerableCounter;
    [Header("是否受伤无敌")]
    //是否受伤无敌
    public bool invulnerable;

    [Header("血量变更")]

    //创建一个unity事件通过面板把事件广播出去
    public UnityEvent<Character> onHealthChange;

    //创建一个unity事件，用来一次判断执行多个方法
    [Header("受伤击退")]
    public UnityEvent<Transform> OnTakeDamage;

    [Header("死亡")]
    public UnityEvent OnDie;

    //脚本开始执行时，让当前血量等于最大血量
    private void Start()
    {
        //游戏开始时滑铲能量值满
        currentPower = maxPower;
        currentHealth = maxHealth;
        //游戏开始时血量变更为满血
        onHealthChange?.Invoke(this);

    }

    void Update()
    {
        //物体处于无敌状态计时器
        if (invulnerable)
        {
            //counter开始计时持续不断的减去时间修正直到=0
            invulnerableCounter -= Time.deltaTime;
            //如果时间<=0的话 就让物体回到非无敌状态
            if (invulnerableCounter <= 0)
            {
                //结束无敌状态，再次接收TakeDamage方法的伤害
                invulnerable = false;
            }
        }

        //恢复滑铲能量
        if (currentPower < maxPower)
        {
            currentPower += Time.deltaTime * powerRecoverSpeed;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //查找标签为水面
        if (other.CompareTag("Water"))
        {

            //血量更新事件
            currentHealth = 0;
            onHealthChange?.Invoke(this);
            //直接触发死亡OnDie事件
            OnDie?.Invoke();
        }

    }

    //接收伤害方法
    public void TakeDamage(Attack attacker)
    {
        //如果接受伤害的时候物体处于无敌状态，就return不执行后续的代码
        if (invulnerable)
        {
            return;
        }
        //计时器判断当前血量承受伤害后是否>0
        if (currentHealth - attacker.damage > 0)
        {
            //每次受到伤害，当前血量减去伤害值
            currentHealth -= attacker.damage;
            //当受到一次伤害的时候触发无敌方法
            TriggerInvulnerable();
            //执行所有受伤过来的方法
            OnTakeDamage?.Invoke(attacker.transform);
        }
        else
        {
            currentHealth = 0;
            //触发死亡
            OnDie?.Invoke();
        }
        //受伤后把自己传递进去用来执行血量变更
        onHealthChange?.Invoke(this);

    }
    //触发无敌方法（防止人物或怪物穿过身体时连续触发伤害导致死亡）
    private void TriggerInvulnerable()
    {
        //如果不是无敌状态
        if (!invulnerable)
        {
            //进入无敌状态并触发在update中的无敌计时器
            invulnerable = true;
            //无敌状态的时间
            invulnerableCounter = invulnerableDurion;
        }
    }

    //滑铲能量方法
    public void OnSlide(int cost)
    {
        //当前能量减去传递进来的值（消耗能量）
        currentPower -= cost;
        //把自己传递到监听，用来执行能量变更
        onHealthChange?.Invoke(this);
    }
}
