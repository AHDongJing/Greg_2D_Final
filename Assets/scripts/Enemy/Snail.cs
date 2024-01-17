using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snail : Enemy
{
    //重写 enemy中的awake方法，
    protected override void Awake()
    {
        //在enmey awake方法都执行的前提下
        base.Awake();
        //创建一个蜗牛的状态机脚本的实例
        patrolState = new SnailPatrolState();
        skillState = new SnailSkillState();

    }
}
