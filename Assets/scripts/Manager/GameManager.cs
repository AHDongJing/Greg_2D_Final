using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //����
    public static GameManager Instance;
    //��ҵڶ�����������ʼ����
    public Vector3 nextLevelPos;
    //���object
    public GameObject player;
    //Main Camera
    public GameObject mainCamera;
    //playerStasUI
    public GameObject playerStasUI;
    //playerDieUI
    public GameObject playerDieUI;
    //playerBuffUI

    //playerWinUI

    //�³����е��������
    public GameObject nextSceneConfiner;
    

    private void Awake()
    {
        if(Instance != null) 
        {
            Destroy(this);
        }
        Instance = this;

        //��ʼ����Ϸ
        InitNewSceneObj();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //���ص��µĳ���ʱ����Ҫ������object
    public void InitNewSceneObj()
    {
        //�������
        GameObject.DontDestroyOnLoad(mainCamera);
        //Player
        GameObject.DontDestroyOnLoad(player);
        //Player ���� UI
        GameObject.DontDestroyOnLoad(playerStasUI);
        //player Die UI
        GameObject.DontDestroyOnLoad(playerDieUI);
        //Game manager
        DontDestroyOnLoad(gameObject);

    }


    //���¿�ʼ��Ϸ
    public void RestartGame() 
    {

        //���Ź��ȶ���

        //���������֮ǰ������obj
        if (SceneManager.GetActiveScene().name == "Level_02")
        {
            //���ý�ɫλ��
            player.transform.position = nextLevelPos;
            //���ý�ɫ��ǰѪ��
            player.GetComponent<Character>().currentHealth = player.GetComponent<Character>().maxHealth;
            //����Ѫ��
            UIManager.Instance.OnHealthEvent(player.GetComponent<Character>());
            //�����ɫ
            player.GetComponent<PlayerController>().isDead = false;
            player.GetComponent<PlayerController>().inputcontrol.Gameplayer.Enable();
            //�ر�UI
            playerDieUI.SetActive(false);
        }

        //���������֮ǰ������obj
        else
        {
            Destroy(mainCamera);
            Destroy(player);
            Destroy(playerStasUI);
            Destroy(playerDieUI);
            //���¿�ʼ��Ϸʱ��ɾ��manager ���ڵ�obj
            Destroy(gameObject);
        }
        //���¼��ص�ǰ����
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //�˳���Ϸ
    public void ExitGame()
    {
        Debug.Log("�˳���Ϸ");
    }

    
}
