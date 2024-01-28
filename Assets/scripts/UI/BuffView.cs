using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//单个buff 框，挂载在每一个互动的buff 按钮上
public class BuffView : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    //buff config
    public BuffData BuffData;
    //调用buff display
    private BuffDisplay buffDisplay;
    //调用UI_buff 中的方法
    private PlayerBuff_UI UI_buff;

    private void OnEnable()
    {
        UI_buff = transform.root.GetComponent<PlayerBuff_UI>();
    }
    //设置buff 数据
    public void SetBuffData(BuffData buffData,BuffDisplay buffDisplay)
    {
        this.BuffData = buffData;
        this.buffDisplay = buffDisplay;
    }


    //点击Buff
    //增加角色相应属性
    public void OnPointerClick(PointerEventData eventData)
    {
        //减少魔法点
        UI_buff.OnPointChange();
    }

    //悬停在Buff上
    //播放buff文字介绍
    public void OnPointerEnter(PointerEventData eventData)
    {
        //显示buff 信息
        this.buffDisplay.SetDisplayInfo(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //删除buff 信息
        this.buffDisplay.buffDiscription.text = null;
    }

}
