using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//���buff ����
[System.Serializable]
public class BuffData
{
    //buff ����
    public string name;
    //buff ��Ҫ���ĵ���
    public int costPoint;
    //buff ����
    public string info;

    //���캯��
    public BuffData(string name, int costPoint, string info)
    { 
        this.name = name;
        this.costPoint = costPoint;
        this.info = info;
    }
}

