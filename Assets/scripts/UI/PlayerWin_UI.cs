using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWin_UI : MonoBehaviour
{
    //animation
    private Animator anim;
    private void OnEnable()
    {
        //��λanimator
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

    //��Ҫ��UI��ʱִ��
    //----������Ҫ�ýӿڻ�����������д
    private void OnOpen() 
    {
        //���Ŷ���
        anim.CrossFade("UI_Win", 0, 0);
        //��ʾ������
        //buff չʾ

    }

}
