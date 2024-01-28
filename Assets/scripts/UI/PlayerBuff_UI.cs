using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerBuff_UI : MonoBehaviour
{
    //获得玩家
    public GameObject player;
    //天赋点显示
    public Text manaText;
    //动画组件
    private Animator anim;
    //buff 介绍动画
    private Tween buffTween;

    private void OnEnable()
    {
        //定位animator
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
        
        //播放UI 动画
        anim.CrossFade("UI_Idle", 0, 0);
    }

    //玩家天赋点减少
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
