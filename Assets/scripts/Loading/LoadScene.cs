using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    //加载下一个场景
    public string nextSceneName;
    //开始按钮
    public Button startBtn;
    //setting 按钮
    public Button settingBtn;
    //credit 按钮
    public Button creditBtn;
    //exit 按钮
    public Button exitBtn;
    // Start is called before the first frame update
    void Start()
    {
        //开始加载
        startBtn.onClick.AddListener(StartLoading);
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    //进入加载页面
    void StartLoading()
    { 
        OnLoad.nextSceneName = nextSceneName;
        SceneManager.LoadScene("Loading_Scene");
    }

}
