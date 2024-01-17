using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Push : MonoBehaviour
{
    //获得刚体
    private Rigidbody2D rb;
    //推动摆锤的力
    public float force;

    // Start is called before the first frame update
    void Start()
    {
        //获得刚体
        rb = GetComponent<Rigidbody2D>();
        //推动摆锤
        rb.AddForce(Vector2.left*force);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
