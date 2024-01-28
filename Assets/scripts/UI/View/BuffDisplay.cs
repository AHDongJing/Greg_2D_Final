using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;


//buff ��Ϣ��ʾ�򣬹�����ʾ��Ϣ�ĶԻ�����(buff discription)
public class BuffDisplay : MonoBehaviour
{
    //buff ��ʾ����
    public Text buffDiscription;
    //����buff view ����
    private BuffView buffView;
    //tween ����
    private Tween tween;

    //������ʾ��Ϣ
    public void SetDisplayInfo(BuffView buffView)
    { 
        //���buffview �ű�
        this.buffView = buffView;
        //���buff ��ʾ��Ϣ
        string buffDiscriptionText = buffView.BuffData.info;
        //�ڶԻ�������ʾbuff ��Ϣ
        tween = buffDiscription.DOText(buffDiscriptionText,1,true).SetRelative().SetEase(Ease.Linear).SetAutoKill(true);
        tween.Play();

    }
    
}
