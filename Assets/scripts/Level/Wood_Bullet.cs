using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood_Bullet : Level_Bullet
{

    private Rigidbody2D rb;
    public float force;
    //��ת����
    private int rotationOffset = 120;


    //����˺��Ľӿ�
    //����
    public override void Hit()
    {
        
    }


    //�ӵ���������
    public override IEnumerator LifeTime()
    {
        //�����ӵ����ʱ��
        yield return new WaitForSeconds(1);
        //�����ӵ�
        ObjectPoolManager.Instance.Set("Level_Bullet", gameObject);
    }

    //�ӵ��ƶ�
    public override void Move()
    {

        if (target != null)
        {
            if (TryGetComponent<Rigidbody2D>(out rb))
            {
                //�ӵ�����
                //Ŀ������ӵ���λ
                Vector3 dir = target.transform.position - transform.position;
                //�ƶ��ӵ� 
                rb.velocity = new Vector2(dir.x, dir.y).normalized * force;
                //��Ŀ��֮��ĽǶ�
                float rot = Mathf.Atan2(-dir.y, -dir.x)*Mathf.Rad2Deg;
                //��ת�ӵ�
                transform.rotation = Quaternion.Euler(0, 0, rot + rotationOffset);
                
            }
        }

    }
}

