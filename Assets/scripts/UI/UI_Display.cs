using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//������ӵ�UI manager ��
public class UI_Display : MonoBehaviour
{
    public Slider slider;

    //��������ʱ���ί��
    private void OnEnable()
    {
        OnLoad.OnLoadingProgress += DisplayProgress;
    }

    //��ʾ������
    void DisplayProgress(int progress)
    {
        slider.value = progress;
    }

    //��ʾѪ��
    //��ʾ������

    //�������ʱ�Ƴ�ί��
    private void OnDisable()
    {
        OnLoad.OnLoadingProgress -= DisplayProgress;
    }

}
