using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //用于把血量百分比传递进去,在hierarchy直接把playerStatBar物体拖进去
    public PlayerStatBar playerStatBar;

    [Header("事件监听")]
    public CharacterEventSO healthEvent;

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
    private void OnHealthEvent(Character character)
    {
        //血量百分比，当前血量/最大血量
        var persentage = character.currentHealth / character.maxHealth;
        //把血量百分比传递给接收方法
        playerStatBar.OnHealthChange(persentage);
        //把能量条传递给接收方法
        playerStatBar.OnPowerChange(character);
    }
}
