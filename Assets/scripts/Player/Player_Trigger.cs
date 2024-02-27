using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using TransitionsPlus;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Player_Trigger : MonoBehaviour
{
    public GameObject transitionAnim;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other)
    {

        if (other.CompareTag("Portal"))
        {
            //Load scene
           StartCoroutine(MoveToNextScene());
            
        }
    }


    IEnumerator MoveToNextScene()
    {

        transitionAnim.SetActive(true);
        yield return new WaitForSeconds(2);
        transform.parent.position = GameManager.Instance.nextLevelPos;

        Camera.main.transform.Find("VC").GetComponent<CinemachineConfiner2D>().m_BoundingShape2D = GameManager.Instance.nextSceneConfiner.GetComponent<PolygonCollider2D>();
    }

}
