using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("伤害值")]
    public int damage;
    [Header("攻击范围")]
    public float attackRange;
    [Header("攻击频率")]
    public float attackRate;
    //触发器当一个物体碰撞到带有Attack脚本的时候执行这个触发器
    public virtual void OnTriggerStay2D(Collider2D other)
    {

        other.GetComponent<Character>()?.TakeDamage(this);
    }
}
