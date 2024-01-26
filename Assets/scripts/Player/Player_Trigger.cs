using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Player_Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //��ɫ�����ɽ�����Ʒ
    //���䣬������
    private void OnTriggerStay2D(Collider2D other)
    {
        //������������ţ����뵽��һ������
        if (other.CompareTag("Portal"))
        {
            //Load scene
            MoveToNextScene("Level_02");
            
        }
    }

    //�������س���
    private void MoveToNextScene(string sceneName)
    {
        
        SceneManager.LoadScene(sceneName);
        //����������³����е�λ������
        transform.parent.position = GameManager.Instance.nextLevelPos;
        //����������³�����confiner
        Camera.main.transform.Find("VC").GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = GameManager.Instance.nextSceneConfiner.GetComponent<PolygonCollider2D>();

    }
}
