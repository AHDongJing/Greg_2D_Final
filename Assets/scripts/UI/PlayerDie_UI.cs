using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDie_UI : MonoBehaviour
{
    //���ö������
    private Animator anim;
    // Start is called before the first frame update
    private void OnEnable()
    {
        //�õ������animator ���
        anim = transform.Find("Image").GetComponent<Animator>();
        //ִ�п�ʼ����
        OnOpen();
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //��UI��ʱִ��
    void OnOpen() 
    {
        //��UI �в�����������
        anim.SetBool("isDead", true);
    }
}
