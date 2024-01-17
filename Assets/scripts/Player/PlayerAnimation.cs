using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    public Animator anim;
    private Rigidbody2D rd;

    private PhisycsCheck phisycsCheck;

    private PlayerController playerController;

    public void Awake()
    {
        //获取动画组件
        anim = GetComponent<Animator>();
        //获取rigidbody组件
        rd = GetComponent<Rigidbody2D>();
        //获得物理检测脚本(PhisycsCheck)中设置好的所有变量
        phisycsCheck = GetComponent<PhisycsCheck>();

        //获取playerController脚本中设置好的所有变量
        playerController = GetComponent<PlayerController>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        SetAnimation();
    }

    //每一帧实时所有动画切换
    public void SetAnimation()
    {
        //设置动画的速度，("对应的动画组件参数变量名",要传递的数值)这里要传递的数值为物体的刚体x轴移动速度
        //通过MathF.Abs取x轴的绝对值，让velocityX可以小于负数的时候也能执行动画
        //当velocityX的值大于0.1的时候就切换动画到Run(由走路到跑步的切换)
        anim.SetFloat("velocityX", MathF.Abs(rd.velocity.x));
        //跳跃
        anim.SetFloat("velocityY", rd.velocity.y);
        //把碰触地面的检测返回值赋值给跳跃动画的isGround,如果跳跃动画的isGround为false就执行跳跃动画，碰触地面的检测为false说明人物在空中
        anim.SetBool("isGround", phisycsCheck.isGround);
        //把人物下蹲时的检测赋值给动画的isCrouch，如果下蹲动画的isCrouch为true就执行下蹲动画
        anim.SetBool("isCrouch", playerController.isCrouch);
        //把人物死亡时的检测赋值给动画的isDead，如果人物死亡isDead为true就执行死亡动画
        anim.SetBool("isDead", playerController.isDead);
        //把攻击的检测赋值给动画的isAttack如果为true就是正在进行攻击
        anim.SetBool("isAttack", playerController.isAttack);
        //把爬墙检测的返回值给到动画如果为true就执行爬墙动画
        anim.SetBool("onWall", phisycsCheck.onWall);
        //滑铲动画执行把返回值给到动画如果为true就执行滑铲动画
        anim.SetBool("isSlide", playerController.isSlide);


    }
    //受伤动画
    public void PlayHurt()
    {
        anim.SetTrigger("hurt");
    }
    //攻击动画
    public void PlayAttack()
    {
        anim.SetTrigger("attack");
    }
}
