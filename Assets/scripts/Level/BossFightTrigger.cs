using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//触发boss战
//记录小怪战斗时间
public class BossFightTrigger : MonoBehaviour
{
    //调用player stas 来获得其中的游戏时间
    private PlayerStatBar playStats;

    private void OnEnable()
    {
        playStats = GameObject.Find("UI_Player_Property").GetComponent<PlayerStatBar>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //记录boss 战前所用时间
            UI_Win_Manager.instance.minionTime = playStats.elapsedTime;
            Debug.Log(UI_Win_Manager.instance.minionTime);
        }
    }
}
