using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerBuff_UI : MonoBehaviour
{
    //buff 按钮
    public Button buff1;
    //动画组件
    private Animator anim;
    //buff 介绍动画
    private Tween buffTween;

    private void OnEnable()
    {
        //定位animator
        anim = gameObject.GetComponentInChildren<Animator>();
        OnOpen();
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnOpen()
    {
        
        //播放UI 动画
        anim.CrossFade("UI_Idle", 0, 0);
    }

    //第一个buff 被选择
    void OnBuff1()
    { 
    
    }
}
