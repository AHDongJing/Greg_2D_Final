using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Push : MonoBehaviour
{
    //��ø���
    private Rigidbody2D rb;
    //�ƶ��ڴ�����
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        //��ø���
        rb = GetComponent<Rigidbody2D>();
        //�ƶ��ڴ�
        rb.AddForce(Vector2.left*force);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
