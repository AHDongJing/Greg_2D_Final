using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class Boss2GenerateSword : MonoBehaviour
{
    public GameObject gameObject;

    public Transform GeneratePosition;

    public Transform GeneratePosition2;
    //飞剑的数量
    public int numberOfAttack;
    //飞剑数量最大值
    public int numberOfAttackMax;
    public float gapTimeMax = 0.2f;
    public float gapTimer;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            ReleaseSKill();
            gapTimer = gapTimeMax;
        }
        if (numberOfAttack > 0)
            gapTimer -= Time.deltaTime;
        if (numberOfAttack > 0 && gapTimer <= 0)
        {
            gapTimer = gapTimeMax;
            numberOfAttack--;
            float x = Random.Range(GeneratePosition.position.x, GeneratePosition2.position.x);
            GameObject sword = Instantiate(gameObject);
            gameObject.transform.position = new Vector2(x, GeneratePosition.position.y);
        }
    }
    public void ReleaseSKill()
    {
        numberOfAttack = numberOfAttackMax;
    }

}
