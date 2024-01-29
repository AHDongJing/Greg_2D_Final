using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

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

    //调用player stas 来获得其中的游戏时间
    private PlayerStatBar playStats;

    //小怪消耗时间
    public float minionTime;



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
            //打开结算界面
            winUI.gameObject.SetActive(true);
            isOn = true;
            //获得游戏时间数据
            playStats = GameObject.Find("UI_Player_Property").GetComponent<PlayerStatBar>();
            //获得游戏总时间
            float playTime = playStats.elapsedTime;
            //显示时间占比
            winUI.GetComponentInChildren<Slider>().value = minionTime / playTime;
        }
        else
            return;
    }

    
   /* //玩家开启boss 战斗后自动启动---
    public void OnBossFight()
    { 
    
    }*/

    


}
