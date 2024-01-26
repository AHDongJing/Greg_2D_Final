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

    //角色触发可交互物品
    //宝箱，传送门
    private void OnTriggerStay2D(Collider2D other)
    {
        //如果触发传送门，进入到下一个场景
        if (other.CompareTag("Portal"))
        {
            //Load scene
            MoveToNextScene("Level_02");
            
        }
    }

    //触发加载场景
    private void MoveToNextScene(string sceneName)
    {
        
        SceneManager.LoadScene(sceneName);
        //设置玩家在新场景中的位置坐标
        transform.parent.position = GameManager.Instance.nextLevelPos;
        //设置相机在新场景的confiner
        Camera.main.transform.Find("VC").GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = GameManager.Instance.nextSceneConfiner.GetComponent<PolygonCollider2D>();

    }
}
