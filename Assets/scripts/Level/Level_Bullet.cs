using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Level_Bullet : MonoBehaviour
{
    public float damageValue;//�ӵ��˺�
    public GameObject target;//�ӵ�Ŀ��

    //�ӵ���ʼ��
    public virtual void Init(float damageValue, GameObject target)
    { 
        this.damageValue = damageValue;
        this.target = target;
        //�ӵ����ɺ��ƶ�
        Move();
    }

    public abstract void Move();//�ӵ��ƶ�
    public abstract void Hit(); //�ӵ�����,�Խ�ɫ����˺��Ľӿ�
    public abstract IEnumerator LifeTime(); //�ӵ���������

    private void OnEnable() //�ӵ�����ʱִ������
    {
        StartCoroutine("LifeTime");
    }
}
