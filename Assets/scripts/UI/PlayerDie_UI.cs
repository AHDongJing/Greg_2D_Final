using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDie_UI : MonoBehaviour
{
    //���¿�ʼ��Ϸ��ť
    public Button restartBtn;
    //�Ƴ���Ϸ��ť
    public Button exitBtn;
    // Start is called before the first frame update
    private void OnEnable()
    {
        //ִ�п�ʼ����
        OnOpen();
        //restart ��ť�¼�
        restartBtn.onClick.AddListener(GameManager.Instance.RestartGame);
        //exit ��ť�¼�
        exitBtn.onClick.AddListener(GameManager.Instance.ExitGame);
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //��Ҫ��UI��ʱִ�еķ���
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
