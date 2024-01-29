using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//����bossս
//��¼С��ս��ʱ��
public class BossFightTrigger : MonoBehaviour
{
    //����player stas ��������е���Ϸʱ��
    private PlayerStatBar playStats;

    private void OnEnable()
    {
        playStats = GameObject.Find("UI_Player_Property").GetComponent<PlayerStatBar>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //��¼boss սǰ����ʱ��
            UI_Win_Manager.instance.minionTime = playStats.elapsedTime;
            Debug.Log(UI_Win_Manager.instance.minionTime);
        }
    }
}
