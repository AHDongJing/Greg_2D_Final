using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

//����boss ս�е�UI ����
public class UI_Win_Manager : MonoBehaviour
{
    //����
    public static UI_Win_Manager instance;
    //����UI
    public PlayerWin_UI winUI;
    //boss Ѫ��UI
    public BossHealthUI bossHealthUI;

    //�жϽ�������Ƿ��
    private bool isOn = false;

    //����player stas ��������е���Ϸʱ��
    private PlayerStatBar playStats;

    //С������ʱ��
    public float minionTime;



    private void Awake()
    {
        if (instance != null)
        { 
            Destroy(instance);
        }

        instance = this;
    }

    //���ʤ��ʱ��ʤ���������
    public void OnPlayerWin() 
    {
        if (isOn == false)
        {
            //�򿪽������
            winUI.gameObject.SetActive(true);
            isOn = true;
            //�����Ϸʱ������
            playStats = GameObject.Find("UI_Player_Property").GetComponent<PlayerStatBar>();
            //�����Ϸ��ʱ��
            float playTime = playStats.elapsedTime;
            //��ʾʱ��ռ��
            winUI.GetComponentInChildren<Slider>().value = minionTime / playTime;
        }
        else
            return;
    }

    
   /* //��ҿ���boss ս�����Զ�����---
    public void OnBossFight()
    { 
    
    }*/

    


}
