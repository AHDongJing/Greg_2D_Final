using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
//创建一个资产文件
[CreateAssetMenu(menuName = "Event/CharacterEventSO")]

//接收所有委托
public class CharacterEventSO : ScriptableObject
{
    //创建一个委托类型是 Character 委托名 OnEventRaised 这也是一个订阅方法
    public UnityAction<Character> OnEventRaised;
      
    //这个方法用来启动事件,谁需要启动这个事件就把自己身上的Character代码传递进来
    public void RaiseEvent(Character character)
    {
        //确定是否有事件，传递对应的参数
        OnEventRaised?.Invoke(character);
    }


}
