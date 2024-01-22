using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackEnemy : Attack
{

    //触发器当一个物体碰撞到带有Attack脚本的时候执行这个触发器
    public override void OnTriggerStay2D(Collider2D other)
    {
        //获取character脚本下的TakeDamage方法，传入一个参数当前的attacker
        //当碰撞器触发时检测对方身上的Character脚本中的Takedamage方法(?表示不为空确保对方身上挂有Character脚本后才会执行后面的方法)
        if (other.TryGetComponent<EnemyTakeDamage>(out EnemyTakeDamage playerTakeDamage))
        {
            other.GetComponent<Character>()?.TakeDamage(this);
        }
    }
}
