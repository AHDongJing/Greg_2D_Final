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

    public PlayAudioEventSO JumpEvent;

    [Header("组件")]
    //背景音乐变量
    public AudioSource BGMSource;
    //攻击音乐变量
    public AudioSource FXSource;

    public AudioSource JumpSource;


    private void OnEnable()
    {
        //函数注册到事件启动中
        FXEvent.OnEventRaised += OnFXEvent;
        BGMEvent.OnEventRaised += OnBGMEvent;
        JumpEvent.OnEventRaised += OnJumpEvent;
    }

    private void OnDisable()
    {
        FXEvent.OnEventRaised -= OnFXEvent;
        BGMEvent.OnEventRaised -= OnBGMEvent;
        JumpEvent.OnEventRaised -= OnJumpEvent;
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

    //跳跃音频
    private void OnJumpEvent(AudioClip clip)
    {
        //要播放的片段是事件传递进来的片段
        JumpSource.clip = clip;
        //单次播放音频
        JumpSource.Play();
    }


}
