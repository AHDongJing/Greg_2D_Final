using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatBar : MonoBehaviour
{
    //血量条（绿色）
    public Slider healthBar;

    //能量条,idle 一段时间会回复
    public Slider powerBar;
    //Mana point

    //是否恢复能量值
    private bool isRecovering;

    //临时变量保存传递进来的character
    private Character currentCharacter;

    //显示游戏时间
    public Text timeText;
    public float elapsedTime;
    //是否停止记时
    public bool isPause = false;
    private void Update()
    {
        //记时
        OnTimeChange();
        //进入恢复状态
        if (isRecovering)
        {
            //得到恢复值
            float persentage = currentCharacter.currentPower / currentCharacter.maxPower;
            //把恢复值给到ui面板上的 value进行ui变化
            powerBar.value = persentage;
            //当persentage>1代表回复满了
            if (persentage > 1)
            {
                //退出恢复状态
                isRecovering = false;
                return;
            }
        }

    }

    /// <summary>
    /// 接收Health血量变更百分比
    /// </summary>
    /// <param name="presentage">百分比：当前血量/最大血量</param>
    public void OnHealthChange(float persentage)
    {
        //传递进来的参数presentage用来控制血量条图片的填充变化
        healthBar.value = persentage;
    }
    //滑铲能量恢复方法
    public void OnPowerChange(Character character)
    {
        //进入恢复状态
        isRecovering = true;
        currentCharacter = character;
    }

    //显示游戏时间
    public void OnTimeChange() 
    {
        if (!isPause)
        {
            //计算游戏时间
            elapsedTime += Time.deltaTime;
            int min = Mathf.FloorToInt(elapsedTime / 60);
            int sec = Mathf.FloorToInt(elapsedTime % 60);
            //将时间转化为string
            timeText.text = string.Format("{0:00}:{1:00}", min, sec);
        }

        else
            return;
    }




}
