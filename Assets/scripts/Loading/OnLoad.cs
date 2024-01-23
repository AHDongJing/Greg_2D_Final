using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class OnLoad : MonoBehaviour
{
    //��һ��Ҫ���صĳ���
    public static string nextSceneName;
    //�첽����
    private AsyncOperation async;
    //��̨���ؽ���
    private int toProgress;
    //��ʾ���ؽ���
    private int displayProgress;
    //ί��,UI ����
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

    //���ط���
    IEnumerator Load()
    {
        //������̨����
        async = SceneManager.LoadSceneAsync(nextSceneName);
        //���򿪼��صĳ���
        async.allowSceneActivation = false;

        //���90%
        while (async.progress < 0.9f)
        {
            //������ת��Ϊ����
            toProgress = (int)(async.progress * 100);
            //����ʾ����<��̨����ʱ
            while (displayProgress < toProgress)
            {
                displayProgress++;

                //��ί�в�Ϊ��            
                if (OnLoadingProgress != null)
                {
                    //��ʾloading ��ֵ
                    OnLoadingProgress(displayProgress);
                    //ÿ֡����ʱ����
                    yield return new WaitForEndOfFrame();
                }
            }
            //��̨Ҳ��ÿ֡ĩ����
            yield return new WaitForEndOfFrame();
        }
        //������ɣ�����̨������Ϊ100
        toProgress = 100;
        //����ʾ����<��̨���ȣ���ʾ���ȼ�������
        while (displayProgress < toProgress)
        {
            displayProgress++;
            //��ί�в�Ϊ��            
            if (OnLoadingProgress != null)
            {
                //��ʾloading ��ֵ
                OnLoadingProgress(displayProgress);
                //ÿ֡����ʱ����
                yield return new WaitForEndOfFrame();
            }
        }
        //��ʾ�������
        async.allowSceneActivation = true;
    }
}
