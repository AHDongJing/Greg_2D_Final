using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerBuff_UI : MonoBehaviour
{
    //buff ��ť
    public Button buff1;
    //�������
    private Animator anim;
    //buff ���ܶ���
    private Tween buffTween;

    private void OnEnable()
    {
        //��λanimator
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
        
        //����UI ����
        anim.CrossFade("UI_Idle", 0, 0);
    }

    //��һ��buff ��ѡ��
    void OnBuff1()
    { 
    
    }
}
