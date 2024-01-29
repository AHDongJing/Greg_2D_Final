using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWin_UI : MonoBehaviour
{
    //animation
    private Animator anim;
    private void OnEnable()
    {
        //定位animator
        anim = gameObject.GetComponentInChildren<Animator>();
    }
    // Start is called before the first frame update
    void Start()
    {
        OnOpen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //需要在UI打开时执行
    //----后期需要用接口或抽象类进行重写
    private void OnOpen() 
    {
        //播放动画
        anim.CrossFade("UI_Win", 0, 0);
        //显示进度条
        //buff 展示

    }

}
