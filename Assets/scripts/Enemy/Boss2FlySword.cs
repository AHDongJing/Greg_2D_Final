using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss2FlySword : MonoBehaviour
{
    //飞剑下落速度
    public float speed;

    void Start()
    {

    }
    void Update()
    {
        transform.position -= new Vector3(0, speed * Time.deltaTime, 0);
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("SwordDismiss"))
        {
            Destroy(gameObject);
        }
    }
}
