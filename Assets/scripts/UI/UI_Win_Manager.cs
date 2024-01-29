using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//管理boss 战中的UI 激活
public class UI_Win_Manager : MonoBehaviour
{
    //单例
    public static UI_Win_Manager instance;
    //结算UI
    public PlayerWin_UI winUI;
    //boss 血量UI
    public BossHealthUI bossHealthUI;


    //判断结算界面是否打开
    private bool isOn = false;

    private void Awake()
    {
        if (instance != null)
        { 
            Destroy(instance);
        }

        instance = this;
    }

    //玩家胜利时打开胜利结算界面
    public void OnPlayerWin() 
    {
        if (isOn == false)
        {
            winUI.gameObject.SetActive(true);
            isOn = true;
        }
        else
            return;
    }

    //玩家开启boss 战斗后自动启动
    public void OnBossFight()
    { 
    
    }


}
