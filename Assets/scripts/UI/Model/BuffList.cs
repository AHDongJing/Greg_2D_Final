using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//����buff �ļ�
[CreateAssetMenu(fileName = "BuffData.asset", menuName = "CreateBuffData")]
public class BuffList : ScriptableObject
{

        public List<BuffData> buffs;

        //���캯��
        public BuffList()
        {
            buffs = new List<BuffData>();
        }
    
}
