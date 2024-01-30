using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//ͨ��manager ���ͬ�Ĺ�������
public class TransitionManager : MonoBehaviour
{
    //����
    public static TransitionManager Instance;
    //���ض���
    public GameObject loadAnimation;
    //��������
    public GameObject passAnimation;
    
    private void Awake()
    {
        if (Instance != null)
        { 
            Destroy(Instance.gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


}
