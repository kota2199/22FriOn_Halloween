using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoftGear.Strix.Unity.Runtime;

public class TimeManager : StrixBehaviour
{
    public static TimeManager instance;

    [StrixSyncField]
    public bool isPlay = false;

    [StrixSyncField]
    public float time = 300;

    [SerializeField]
    Text timerText;

    [SerializeField]
    GameObject gameOverUi;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    private void Start()
    {
        timerText.text = time.ToString() + "�b";
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            time -= Time.deltaTime;
            timerText.text = time.ToString("f0") + "�b";
        }
        if(time <= 0f)
        {
            isPlay = false;
            time = 0;
            gameOverUi.SetActive(true);
        }

    }

    public void CountStart()
    {
        isPlay = true;
    }
}
