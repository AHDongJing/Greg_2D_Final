using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;


//buff 信息显示框，挂在显示信息的对话框中(buff discription)
public class BuffDisplay : MonoBehaviour
{
    //buff 显示区域
    public Text buffDiscription;
    //调用buff view 方法
    private BuffView buffView;
    //tween 动画
    private Tween tween;

    //设置显示信息
    public void SetDisplayInfo(BuffView buffView)
    { 
        //获得buffview 脚本
        this.buffView = buffView;
        //获得buff 显示信息
        string buffDiscriptionText = buffView.BuffData.info;
        //在对话框中显示buff 信息
        tween = buffDiscription.DOText(buffDiscriptionText,1,true).SetRelative().SetEase(Ease.Linear).SetAutoKill(true);
        tween.Play();

    }
    
}
