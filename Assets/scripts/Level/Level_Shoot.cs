using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level_Shoot : MonoBehaviour
{
    public GameObject bullet;
    //进入范围的敌人
    public GameObject lockingEnemy;
    //发射子弹的位置
    public Transform bulletPos;
    //子弹伤害
    public float damageValue;
    //计时器
    private float timer = 0;
    //射击间隔
    public float waitTime;


    // Update is called once per frame
    void Update()
    {
        //子弹射击间隔
        timer += Time.deltaTime;

        if (timer > waitTime)
        {
            AttackFire();
            timer = 0;
        }
    }

    //检测敌人/玩家是否进入攻击范围
    //进行索敌
    void OnTriggerEnter2D(Collider2D other)
    {
        //如果玩家进入范围
        if (other.CompareTag("Player"))
        {
            //锁定玩家
            lockingEnemy = other.gameObject;
        }
    }

    //当敌人/玩家离开攻击范围
    //移除目标
    private void OnTriggerExit2D(Collider2D other)
    {
        //玩家离开，清空索敌
        if (other.CompareTag("Player"))
        {
            lockingEnemy = null;
        }
    }


    //发射子弹
    public void AttackFire()
    {
        if (lockingEnemy != null)
        {

            //对象池
            GameObject objRes = bullet;

            //生成子弹对象
            GameObject bulletClone = ObjectPoolManager.Instance.Get("Level_Bullet", objRes, bulletPos.position, Quaternion.identity);

            //子弹脚本
            Wood_Bullet woodBullet;

            //获得子弹上的wood bullet 脚本          
            woodBullet = bulletClone.GetComponent<Wood_Bullet>();
            
            //生成woodBullet 子弹
            woodBullet.Init(damageValue,lockingEnemy);
        }
    }
}

