using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerBuff_UI : MonoBehaviour
{
    //������
    public GameObject player;
    //�츳����ʾ
    public Text manaText;
    //�������
    private Animator anim;
    //buff ���ܶ���
    private Tween buffTween;

    private void OnEnable()
    {
        //��λanimator
        anim = gameObject.GetComponentInChildren<Animator>();
        OnOpen();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnOpen()
    {
        
        //����UI ����
        anim.CrossFade("UI_Idle", 0, 0);
    }

    //����츳�����
    public void OnPointChange()
    {
        int point = player.GetComponent<Character>().currentManaPoint;
        if (point > 0)
        {
            player.GetComponent<Character>().currentManaPoint -= 1;
            point = player.GetComponent<Character>().currentManaPoint;
        }
        else {
            return;
        }

        manaText.text = point.ToString() + " point left";

    }
}
