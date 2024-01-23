using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class OnLoad : MonoBehaviour
{
    //下一个要加载的场景
    public static string nextSceneName;
    //异步加载
    private AsyncOperation async;
    //后台加载进度
    private int toProgress;
    //显示加载进度
    private int displayProgress;
    //委托,UI 调用
    public static Action<int> OnLoadingProgress;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Load());
    }

    // Update is called once per frame
    void Update()
    {

    }

    //加载方法
    IEnumerator Load()
    {
        //创建后台加载
        async = SceneManager.LoadSceneAsync(nextSceneName);
        //不打开加载的场景
        async.allowSceneActivation = false;

        //完成90%
        while (async.progress < 0.9f)
        {
            //将进度转化为整数
            toProgress = (int)(async.progress * 100);
            //当显示进度<后台进度时
            while (displayProgress < toProgress)
            {
                displayProgress++;

                //当委托不为空            
                if (OnLoadingProgress != null)
                {
                    //显示loading 数值
                    OnLoadingProgress(displayProgress);
                    //每帧结束时更新
                    yield return new WaitForEndOfFrame();
                }
            }
            //后台也是每帧末更新
            yield return new WaitForEndOfFrame();
        }
        //加载完成，讲后台更新设为100
        toProgress = 100;
        //当显示进度<后台进度，显示进度继续更新
        while (displayProgress < toProgress)
        {
            displayProgress++;
            //当委托不为空            
            if (OnLoadingProgress != null)
            {
                //显示loading 数值
                OnLoadingProgress(displayProgress);
                //每帧结束时更新
                yield return new WaitForEndOfFrame();
            }
        }
        //显示进度完成
        async.allowSceneActivation = true;
    }
}
