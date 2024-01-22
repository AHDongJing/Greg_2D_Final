using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Shoot : MonoBehaviour
{
    public GameObject bullet;
    //���뷶Χ�ĵ���
    public GameObject lockingEnemy;
    //�����ӵ���λ��
    public Transform bulletPos;
    //�ӵ��˺�
    public float damageValue;
    //��ʱ��
    private float timer = 0;
    //������
    public float waitTime;


    // Update is called once per frame
    void Update()
    {
        //�ӵ�������
        timer += Time.deltaTime;

        if (timer > waitTime)
        {
            AttackFire();
            timer = 0;
        }
    }

    //������/����Ƿ���빥����Χ
    //��������
    void OnTriggerEnter2D(Collider2D other)
    {
        //�����ҽ��뷶Χ
        if (other.CompareTag("Player"))
        {
            //�������
            lockingEnemy = other.gameObject;
        }
    }

    //������/����뿪������Χ
    //�Ƴ�Ŀ��
    private void OnTriggerExit2D(Collider2D other)
    {
        //����뿪���������
        if (other.CompareTag("Player"))
        {
            lockingEnemy = null;
        }
    }


    //�����ӵ�
    public void AttackFire()
    {
        if (lockingEnemy != null)
        {

            //�����
            GameObject objRes = bullet;

            //�����ӵ�����
            GameObject bulletClone = ObjectPoolManager.Instance.Get("Level_Bullet", objRes, bulletPos.position, Quaternion.identity);

            //�ӵ��ű�
            Wood_Bullet woodBullet;

            //����ӵ��ϵ�wood bullet �ű�          
            woodBullet = bulletClone.GetComponent<Wood_Bullet>();
            
            //����woodBullet �ӵ�
            woodBullet.Init(damageValue,lockingEnemy);
        }
    }
}

