using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossManager : MonoBehaviour
{
    public event EventHandler OnBoss1Dead;
    public event EventHandler OnBoss2Dead;

    public Character boss1;
    public Character boss2;

    void Start()
    {
        boss1.onHealthChange.AddListener(Boss1_onHealthChange);
        boss2.onHealthChange.AddListener(Boss2_onHealthChange);
    }


    private void Boss2_onHealthChange(Character arg0)
    {
        if (boss2.currentHealth / boss2.maxHealth < 0.7)
        {
            boss2.GetComponent<Boos2SkillController>().flySwordSkillFlag = true;
        }

    }

    private void Boss1_onHealthChange(Character arg0)
    {

    }

    public static BossManager Instance;

    void Awake()
    {
        Instance = this;

    }
    public void Boss1Dead()
    {
        OnBoss1Dead?.Invoke(this, EventArgs.Empty);
        ShowBoss2();

    }
    public void Boss2Dead()
    {
        OnBoss2Dead?.Invoke(this, EventArgs.Empty);
    }

    //展示二阶段boss
    public void ShowBoss2()
    {
        boss2.gameObject.SetActive(true);
    }
}
