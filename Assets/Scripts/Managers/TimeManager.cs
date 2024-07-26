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
    public float time = 120;

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private GameObject gameOverUi;

    private bool isEnded = false;

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
        timerText.text = time.ToString() + "秒";
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            time -= Time.deltaTime;
            timerText.text = time.ToString("f0") + "秒";
        }
        if(time <= 0f)
        {
            isPlay = false;
            time = 0;
            if (!isEnded)
            {
                int score = ScoreManager.instance.totalScore;
                int yokaiIndex = CollideAndGenerateManager.instance.archiveValue;
                gameOverUi.SetActive(true);
                gameOverUi.GetComponent<ResultUICanvas>().SetActiveResultUICanvas(true, score, yokaiIndex);
                isEnded = true;
            }
        }

    }

    public void CountStart()
    {
        isPlay = true;
    }
}
