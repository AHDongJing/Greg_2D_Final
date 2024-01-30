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
    public GameObject buffUI;
    //playerWinUI

    //�³����е��������
    public GameObject nextSceneConfiner;
    

    private void Awake()
    {
        if(Instance != null) 
        {
            //����������Ѿ����ڵ�obj
            Destroy(Instance.mainCamera);
            Destroy(Instance.player);
            Destroy(Instance.playerStasUI);
            Destroy(Instance.playerDieUI);
            Destroy(Instance.buffUI);
            Destroy(Instance.gameObject);           
            return;
        }

        Instance = this;
        Instance.mainCamera = this.mainCamera;
        Instance.player = this.player;
        Instance.playerStasUI = this.playerStasUI;
        Instance.playerDieUI = this.playerDieUI;
        Instance.buffUI = this.buffUI;

        DontDestroyOnLoad(gameObject);
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
        //player buff UI
        GameObject.DontDestroyOnLoad(buffUI);
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

        //���¼��ص�ǰ����
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //���¼�ʱ
        playerStasUI.GetComponent<PlayerStatBar>().isPause = false;
    }

    //�˳���Ϸ
    public void ExitGame()
    {
        Debug.Log("�˳���Ϸ");
    }

    
}
