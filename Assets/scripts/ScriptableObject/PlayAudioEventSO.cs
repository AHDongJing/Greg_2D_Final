using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Event/PlayAudioEventSO")]
public class PlayAudioEventSO : ScriptableObject
{
    //声明一个泛型委托 类型是AudioClip
    public UnityAction<AudioClip> OnEventRaised;
    //事件监听方法
    public void RaiseEvent(AudioClip audioClip)
    {
        //判断如果不为空就执行监听
        OnEventRaised?.Invoke(audioClip);

    }
}
