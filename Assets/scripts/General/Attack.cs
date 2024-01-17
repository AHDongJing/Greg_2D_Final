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
    private void OnTriggerStay2D(Collider2D other)
    {
        //获取character脚本下的TakeDamage方法，传入一个参数当前的attacker
        //当碰撞器触发时检测对方身上的Character脚本中的Takedamage方法(?表示不为空确保对方身上挂有Character脚本后才会执行后面的方法)
        other.GetComponent<Character>()?.TakeDamage(this);
    }
}
