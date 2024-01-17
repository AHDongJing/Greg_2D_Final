
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/VoidEvnetSO")]
public class VoidEventSO : ScriptableObject
{
    public UnityAction OnEventRaised;
    //广播的函数方法，广播的时候所有订阅的函数方法都会执行订阅事件
    public void RaiseEvent()
    {
        //确定是否有事件，传递对应的参数
        OnEventRaised?.Invoke();
    }
}
