using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boar : Enemy
{
    //重写 enemy中的awake方法，
    protected override void Awake()
    {
        //在enmey awake方法都执行的前提下
        base.Awake();
        //创建一个野猪的状态机脚本的实例
        patrolState = new BoarPatrolState();
        chaseState = new BoarChaseState();
    }


    // //子类重写父类的方法
    // public override void Move()
    // {
    //     //保留父类的方法效果
    //     base.Move();
    //     anim.SetBool("walk", true);
    // }
}
