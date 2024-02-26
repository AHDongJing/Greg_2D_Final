using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerDie_UI : MonoBehaviour
{
    //重新开始游戏按钮
    public Button restartBtn;
    //退出游戏按钮
    public Button exitBtn;
    //打字区域
    public Text textAnim;
    //打字内容
    public string textContent = "you lost, but no worries, keep practice, good luck";
    //tween 动画
    private Tween tween;
    //animation
    private Animator anim;

    // Start is called before the first frame update
    private void OnEnable()
    {
        //定位animator
        anim = gameObject.GetComponentInChildren<Animator>();
    }
    void Start()
    {
        OnOpen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //需要在UI打开时执行的方法
    void OnOpen()
    {
        //播放文字动画
        tween = textAnim.DOText(textContent, 2, true).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
        tween.Play();
        //播放UI 动画
        anim.CrossFade("Die_Animation", 0, 0);
    }

    

}
