using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            winUI.gameObject.SetActive(true);
            isOn = true;
        }
        else
            return;
    }

    //��ҿ���boss ս�����Զ�����
    public void OnBossFight()
    { 
    
    }


}
