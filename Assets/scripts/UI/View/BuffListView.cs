using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffListView : MonoBehaviour
{
    //����buff config
    private BuffList buffDataBase;
    //����buff list
    public List<BuffView> buffs;

    public BuffDisplay buffDisplay;

    // Start is called before the first frame update
    void Start()
    {
        //��ȡbuff ����
        buffDataBase = Resources.Load<BuffList>("BuffData");
        //��ʼ��ÿ��buff
        SetupBuffList(buffDataBase.buffs);
    }


    void SetupBuffList(List<BuffData> datas)
    { 
        //����ÿ��buff ������
        for(int i =0; i< datas.Count; i++) 
        {
            buffs[i].SetBuffData(buffDataBase.buffs[i], buffDisplay);
        }
    }
}
