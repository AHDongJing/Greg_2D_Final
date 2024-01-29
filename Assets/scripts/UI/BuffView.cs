using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//����buff �򣬹�����ÿһ��������buff ��ť��
public class BuffView : MonoBehaviour, IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    //buff config
    public BuffData BuffData;
    //����buff display
    private BuffDisplay buffDisplay;
    //����UI_buff �еķ���
    private PlayerBuff_UI UI_buff;

    private void OnEnable()
    {
        UI_buff = transform.root.GetComponent<PlayerBuff_UI>();
    }
    //����buff ����
    public void SetBuffData(BuffData buffData,BuffDisplay buffDisplay)
    {
        this.BuffData = buffData;
        this.buffDisplay = buffDisplay;
    }


    //���Buff
    //���ӽ�ɫ��Ӧ����
    public void OnPointerClick(PointerEventData eventData)
    {
        //����ħ����
        UI_buff.OnPointChange();
    }

    //��ͣ��Buff��
    //����buff���ֽ���
    public void OnPointerEnter(PointerEventData eventData)
    {
        //��ʾbuff ��Ϣ
        this.buffDisplay.SetDisplayInfo(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //ɾ��buff ��Ϣ
        this.buffDisplay.buffDiscription.text = null;
    }

}
