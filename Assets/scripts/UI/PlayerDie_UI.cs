using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie_UI : MonoBehaviour
{
    //调用动画组件
    private Animator anim;
    // Start is called before the first frame update
    private void OnEnable()
    {
        //得到本身的animator 组件
        anim = transform.Find("Image").GetComponent<Animator>();
        //执行开始方法
        OnOpen();
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //当UI打开时执行
    void OnOpen() 
    {
        //在UI 中播放死亡动画
        anim.SetBool("isDead", true);
    }
}
