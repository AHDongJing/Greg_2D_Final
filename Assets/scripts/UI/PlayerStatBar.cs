using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatBar : MonoBehaviour
{
    //血量条（绿色）
    public Image healthImage;
    //血量扣除延迟条（红色）
    public Image healthDelayImage;

    //能量条
    public Image powerImage;

    //是否恢复能量值
    private bool isRecovering;
    //临时变量保存传递进来的character
    private Character currentCharacter;


    private void Update()
    {
        //如果血量延迟扣除大于血量条
        if (healthDelayImage.fillAmount > healthImage.fillAmount)
        {
            healthDelayImage.fillAmount -= Time.deltaTime;
        }
        //进入恢复状态
        if (isRecovering)
        {
            //得到恢复值
            float persentage = currentCharacter.currentPower / currentCharacter.maxPower;
            //把恢复值给到ui面板上的 fillamount进行ui变化
            powerImage.fillAmount = persentage;
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
    public void OnHealthChange(float presentage)
    {
        //传递进来的参数presentage用来控制血量条图片的填充变化（unity fillamout）
        healthImage.fillAmount = presentage;
    }
    //滑铲能量恢复方法
    public void OnPowerChange(Character character)
    {
        //进入恢复状态
        isRecovering = true;
        currentCharacter = character;
    }


}
