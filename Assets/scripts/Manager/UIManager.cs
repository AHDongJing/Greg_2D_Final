using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    //写单例
    public static UIManager Instance;
    //用于把血量百分比传递进去,在hierarchy直接把playerStatBar物体拖进去
    public PlayerStatBar playerStatBar;
    //玩家死亡时的UI
    public PlayerDie_UI playerDieUI;
    //玩家buff 
    public PlayerBuff_UI playerBuffUI;
    //buff ui 是否已打开
    public bool isOn = false;

    [Header("事件监听")]
    public CharacterEventSO healthEvent;

    private void Awake()
    {
        if (Instance != null)
        { 
            Destroy(Instance);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    //注册事件
    void OnEnable()
    {
        //订阅事件把对应的函数加到这个订阅中
        healthEvent.OnEventRaised += OnHealthEvent;
        
    }

    //取消事件
    void OnDisable()
    {
        //把对应的函数在订阅中取消
        healthEvent.OnEventRaised -= OnHealthEvent;
    }

    //广播执行对应的方法
    //添加和角色health 有关的方法
    public void OnHealthEvent(Character character)
    {
        //血量百分比，当前血量/最大血量
        var persentage = character.currentHealth / character.maxHealth;
        //把血量百分比传递给接收方法
        playerStatBar.OnHealthChange(persentage);
        //把能量条传递给接收方法
        playerStatBar.OnPowerChange(character);
    }

    //角色死亡时打开死亡UI
    public void OnDieEvent()
    { 
        //打开角色死亡时候的UI
        playerDieUI.gameObject.SetActive(true);
    }

    //打开buffUI
    public void OnBuffEvent() 
    {
        //判断ui 是否已经打开
        if (!isOn)
        {
            //打开player buff ui
            playerBuffUI.gameObject.SetActive(true);
            isOn = true;
        }

        else
            OffBuffEvent();
       
    }

    //关闭buffUI
    public void OffBuffEvent()
    {
        //关闭player buff ui
        playerBuffUI.gameObject.SetActive(false);
        isOn = false;
    }


}
