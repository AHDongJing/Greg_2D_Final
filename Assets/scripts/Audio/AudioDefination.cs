using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDefination : MonoBehaviour
{
    //声明一个事件用来传递和广播（这里在unity中放的是配置文件由[CreateAssetMenu(menuName = "Event/PlayAudioEventSO")]创建）
    public PlayAudioEventSO playAudioEvent;
    //音频片段
    public AudioClip audioClip;
    //是否播放音乐
    public bool playOnEnable;

    private void OnEnable()
    {
        //游戏启动时如果 playOnEnable为true
        if (playOnEnable)
        {
            //播放音乐
            PlayAudioClip();
        }
    }

    //播放音乐方法
    public void PlayAudioClip()
    {
        //把音乐片段传递给监听事件
        playAudioEvent.OnEventRaised(audioClip);
    }
}
