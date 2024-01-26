using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDie_UI : MonoBehaviour
{
    //重新开始游戏按钮
    public Button restartBtn;
    //推出游戏按钮
    public Button exitBtn;
    // Start is called before the first frame update
    private void OnEnable()
    {
        //执行开始方法
        OnOpen();
        //restart 按钮事件
        restartBtn.onClick.AddListener(GameManager.Instance.RestartGame);
        //exit 按钮事件
        exitBtn.onClick.AddListener(GameManager.Instance.ExitGame);
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //需要在UI打开时执行的方法
    void OnOpen() 
    {
        Debug.Log("OPEN");
    }

    private void OnDisable()
    {
        restartBtn.onClick.RemoveAllListeners();
        exitBtn.onClick.RemoveAllListeners();
    }

}
