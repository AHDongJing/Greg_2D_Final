using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//生成buff 文件
[CreateAssetMenu(fileName = "BuffData.asset", menuName = "CreateBuffData")]
public class BuffList : ScriptableObject
{

        public List<BuffData> buffs;

        //构造函数
        public BuffList()
        {
            buffs = new List<BuffData>();
        }
    
}
