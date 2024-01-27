using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Boos2SkillController : MonoBehaviour
{
    public Boss2GenerateSword boss2GenerateSword;

    public bool flySwordSkillFlag;

    public float flySwordSkillTimer;

    public float flySwordSkillTimeMax = 20f;

    public bool warningFlag;


    void Start()
    {
        flySwordSkillTimer = 3f;
    }
    void Update()
    {
        //飞剑下落循环
        if (flySwordSkillFlag)
        {
            if (flySwordSkillTimer <= 3f && warningFlag == false)
            {
                warningFlag = true;
                warningEvent?.Invoke();
                Text();
            }
            flySwordSkillTimer -= Time.deltaTime;
            if (flySwordSkillTimer < 0)
            {
                flySwordSkillTimer = flySwordSkillTimeMax;
                boss2GenerateSword.ReleaseSKill();
                warningFlag = false;

            }
        }
    }
    [Header("警告")]
    public UnityEvent warningEvent;
    public void Text()
    {
        Debug.Log("boss ult warning");
    }
}
