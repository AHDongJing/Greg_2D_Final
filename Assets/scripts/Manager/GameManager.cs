using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //单例
    public static GameManager Instance;
    //玩家第二个场景的启始坐标
    public Vector3 nextLevelPos;
    //玩家object
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

    //新场景中的相机限制
    public GameObject nextSceneConfiner;
    

    private void Awake()
    {
        if(Instance != null) 
        {
            //清楚场景中已经存在的obj
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
        //初始化游戏
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

    //加载到新的场景时，需要保留的object
    public void InitNewSceneObj()
    {
        //设置相机
        GameObject.DontDestroyOnLoad(mainCamera);
        //Player
        GameObject.DontDestroyOnLoad(player);
        //Player 属性 UI
        GameObject.DontDestroyOnLoad(playerStasUI);
        //player Die UI
        GameObject.DontDestroyOnLoad(playerDieUI);
        //player buff UI
        GameObject.DontDestroyOnLoad(buffUI);
    }


    //重新开始游戏
    public void RestartGame() 
    {

        //播放过度动画

        //清除场景内之前保留的obj
        if (SceneManager.GetActiveScene().name == "Level_02")
        {
            //重置角色位置
            player.transform.position = nextLevelPos;
            //重置角色当前血量
            player.GetComponent<Character>().currentHealth = player.GetComponent<Character>().maxHealth;
            //重置血条
            UIManager.Instance.OnHealthEvent(player.GetComponent<Character>());
            //复活角色
            player.GetComponent<PlayerController>().isDead = false;
            player.GetComponent<PlayerController>().inputcontrol.Gameplayer.Enable();
            //关闭UI
            playerDieUI.SetActive(false);
        }

        //重新加载当前场景
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //重新记时
        playerStasUI.GetComponent<PlayerStatBar>().isPause = false;
    }

    //退出游戏
    public void ExitGame()
    {
        Debug.Log("退出游戏");
    }

    
}
