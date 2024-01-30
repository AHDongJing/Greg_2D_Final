using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//临时调用，sound manager
public class BackgroundManager : MonoBehaviour
{
    static BackgroundManager current;
    private AudioSource backgroundSource;

    [Header("背景音效")]
    public AudioClip backgroundClip;

    private void Awake()
    {
        if (current != null)
        { 
            Destroy(current);
            return;
        }

        current = this;
        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        backgroundSource = gameObject.AddComponent<AudioSource>();
        StartLevelAudio();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartLevelAudio()
    {
        current.backgroundSource.clip = current.backgroundClip;
        current.backgroundSource.loop = true;
        current.backgroundSource.Play();
    }
}
