using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    //������һ������
    public string nextSceneName;
    //��ʼ��ť
    public Button startBtn;
    //setting ��ť
    public Button settingBtn;
    //credit ��ť
    public Button creditBtn;
    //exit ��ť
    public Button exitBtn;
    // Start is called before the first frame update
    void Start()
    {
        //��ʼ����
        startBtn.onClick.AddListener(StartLoading);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    //�������ҳ��
    void StartLoading()
    { 
        OnLoad.nextSceneName = nextSceneName;
        SceneManager.LoadScene("Loading_Scene");
    }

}
