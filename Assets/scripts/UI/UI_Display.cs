using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


//后期添加到UI manager 中
public class UI_Display : MonoBehaviour
{
    public Slider slider;

    //程序运行时添加委托
    private void OnEnable()
    {
        OnLoad.OnLoadingProgress += DisplayProgress;
    }

    //显示进度条
    void DisplayProgress(int progress)
    {
        slider.value = progress;
    }

    //显示血条
    //显示能量条

    //程序结束时移除委托
    private void OnDisable()
    {
        OnLoad.OnLoadingProgress -= DisplayProgress;
    }

}
