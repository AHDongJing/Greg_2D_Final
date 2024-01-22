using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level_Bullet : MonoBehaviour
{
    public float damageValue;//子弹伤害
    public GameObject target;//子弹目标

    //子弹初始化
    public virtual void Init(float damageValue, GameObject target)
    { 
        this.damageValue = damageValue;
        this.target = target;
        //子弹生成后移动
        Move();
    }

    public abstract void Move();//子弹移动
    public abstract void Hit(); //子弹命中,对角色造成伤害的接口
    public abstract IEnumerator LifeTime(); //子弹生命周期

    private void OnEnable() //子弹生成时执行周期
    {
        StartCoroutine("LifeTime");
    }
}
