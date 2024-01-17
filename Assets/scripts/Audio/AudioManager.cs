using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//引用音频都命名空间
using UnityEngine.Audio;
public class AudioManager : MonoBehaviour
{

    [Header("事件监听")]
    public PlayAudioEventSO FXEvent;
    public PlayAudioEventSO BGMEvent;

    [Header("组件")]
    //背景音乐变量
    public AudioSource BGMSource;
    //攻击音乐变量
    public AudioSource FXSource;


    private void OnEnable()
    {
        //函数注册到事件启动中
        FXEvent.OnEventRaised += OnFXEvent;
        BGMEvent.OnEventRaised += OnBGMEvent;
    }

    private void OnDisable()
    {
        FXEvent.OnEventRaised -= OnFXEvent;
        BGMEvent.OnEventRaised -= OnBGMEvent;
    }

    //bgm音频
    private void OnBGMEvent(AudioClip clip)
    {
        //要播放的片段是事件传递进来的片段
        BGMSource.clip = clip;
        //单次播放音频
        BGMSource.Play();
    }

    //攻击音频
    private void OnFXEvent(AudioClip clip)
    {
        //要播放的片段是事件传递进来的片段
        FXSource.clip = clip;
        //单次播放音频
        FXSource.Play();
    }
}