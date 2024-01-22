using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wood_Bullet : Level_Bullet
{

    private Rigidbody2D rb;
    public float force;
    //旋转补偿
    private int rotationOffset = 120;


    //造成伤害的接口
    //备用
    public override void Hit()
    {
        
    }


    //子弹生命周期
    public override IEnumerator LifeTime()
    {
        //设置子弹存活时间
        yield return new WaitForSeconds(1);
        //回收子弹
        ObjectPoolManager.Instance.Set("Level_Bullet", gameObject);
    }

    //子弹移动
    public override void Move()
    {

        if (target != null)
        {
            if (TryGetComponent<Rigidbody2D>(out rb))
            {
                //子弹方向
                //目标距离子弹方位
                Vector3 dir = target.transform.position - transform.position;
                //移动子弹 
                rb.velocity = new Vector2(dir.x, dir.y).normalized * force;
                //与目标之间的角度
                float rot = Mathf.Atan2(-dir.y, -dir.x)*Mathf.Rad2Deg;
                //旋转子弹
                transform.rotation = Quaternion.Euler(0, 0, rot + rotationOffset);
                
            }
        }

    }
}

