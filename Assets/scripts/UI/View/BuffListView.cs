using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffListView : MonoBehaviour
{
    //调用buff config
    private BuffList buffDataBase;
    //创建buff list
    public List<BuffView> buffs;

    public BuffDisplay buffDisplay;

    // Start is called before the first frame update
    void Start()
    {
        //读取buff 数据
        buffDataBase = Resources.Load<BuffList>("BuffData");
        //初始化每个buff
        SetupBuffList(buffDataBase.buffs);
    }


    void SetupBuffList(List<BuffData> datas)
    { 
        //设置每个buff 的属性
        for(int i =0; i< datas.Count; i++) 
        {
            buffs[i].SetBuffData(buffDataBase.buffs[i], buffDisplay);
        }
    }
}
