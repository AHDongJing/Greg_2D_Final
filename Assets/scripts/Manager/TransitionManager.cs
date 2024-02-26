using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//通过manager 激活不同的过场动画
public class TransitionManager : MonoBehaviour
{
    //单例
    public static TransitionManager Instance;
    //加载动画
    public GameObject loadAnimation;
    //过场动画
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
