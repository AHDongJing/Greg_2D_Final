using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

//玩家buff 数据
[System.Serializable]
public class BuffData
{
    //buff 名字
    public string name;
    //buff 需要消耗点数
    public int costPoint;
    //buff 介绍
    public string info;

    //构造函数
    public BuffData(string name, int costPoint, string info)
    { 
        this.name = name;
        this.costPoint = costPoint;
        this.info = info;
    }
}

