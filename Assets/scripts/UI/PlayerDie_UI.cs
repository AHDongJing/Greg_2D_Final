using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PlayerDie_UI : MonoBehaviour
{
    //���¿�ʼ��Ϸ��ť
    public Button restartBtn;
    //�˳���Ϸ��ť
    public Button exitBtn;
    //��������
    public Text textAnim;
    //��������
    public string textContent = "you lost, but no worries, keep practice, good luck";
    //tween ����
    private Tween tween;
    //animation
    private Animator anim;

    // Start is called before the first frame update
    private void OnEnable()
    {
        //��λanimator
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

    //��Ҫ��UI��ʱִ�еķ���
    void OnOpen()
    {
        //�������ֶ���
        tween = textAnim.DOText(textContent, 2, true).SetRelative().SetEase(Ease.Linear).SetAutoKill(false);
        tween.Play();
        //����UI ����
        anim.CrossFade("Die_Animation", 0, 0);
    }

    

}
