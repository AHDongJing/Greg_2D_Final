using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System;

public class CameraControl : MonoBehaviour
{
    private CinemachineConfiner2D confiner2D;
    //获取CinemachineImpulseSource这个扩展摄像机的里的组件
    public CinemachineImpulseSource impulseSource;
    //监听事件VoidEventSO中的广播事件
    public VoidEventSO cameraShakeEvent;

    private void Awake()
    {
        //获取CinemachineConfiner2D组件
        confiner2D = GetComponent<CinemachineConfiner2D>();
    }

    private void OnEnable()
    {
        //事件接受到订阅广播后 就调用OnCameraShakeEvent方法播放抖动
        cameraShakeEvent.OnEventRaised += OnCameraShakeEvent;
    }

    private void OnDisable()
    {
        cameraShakeEvent.OnEventRaised -= OnCameraShakeEvent;
    }
    private void OnCameraShakeEvent()
    {
        //调用相机抖动的api （这个api是cinemachine自带的）
        impulseSource.GenerateImpulse();
    }

    //TODO：场景切换后更改
    void Start()
    {
        GetNewCameraBounds();
    }

    /// <summary>
    /// 寻找tag方法，用于场景切换时找到tag
    /// </summary>
    private void GetNewCameraBounds()
    {
        //找到标签赋值给obj
        var obj = GameObject.FindGameObjectWithTag("Bounds");

        //添加约束判断是否有对应的tag
        if (obj == null)
        {
            //如果为空就不执行后面的代码
            return;
        }
        //把标签给到colldier （Collider2D代表所有类型的碰撞体）
        confiner2D.m_BoundingShape2D = obj.GetComponent<Collider2D>();
        //获得一个新的图形后清除缓存
        confiner2D.InvalidateCache();
    }
}
