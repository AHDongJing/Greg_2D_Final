using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

public class Level_rotate : MonoBehaviour
{
    private float speed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //ÿ����ת10��
        transform.Rotate(Vector3.forward * speed * Time.deltaTime);
    }
}
