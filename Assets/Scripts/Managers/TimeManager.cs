using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoftGear.Strix.Unity.Runtime;

public class TimeManager : StrixBehaviour
{
    public static TimeManager instance;


    //プレイ中かどうか、経過時間、時間表示用テキストを各クライアントと同期する。
    [StrixSyncField]
    public bool isPlay = false;

    [StrixSyncField]
    public float remainTime = 120;

    [SerializeField]
    private Text timerText;

    [SerializeField]
    private GameObject gameOverUi, resultCanvas;

    //ゲーム終了時処理を一度だけ呼ぶための値
    public bool isEnded = false;

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
        timerText.text = remainTime.ToString() + "秒";
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlay)
        {
            remainTime -= Time.deltaTime;
            timerText.text = remainTime.ToString("f0") + "秒";
        }

        //残り時間が0になったらゲーム終了。
        if(remainTime <= 0f)
        {
            isPlay = false;
            remainTime = 0;
            RpcToAll(nameof(GameEnd));
        }

    }

    //ReadyStatusManagerでカウントダウンが終わると呼び出される
    public void CountStart()
    {
        isPlay = true;
    }

    //ゲームオーバーオブジェクトに接触したときにCollideNotificatorから呼び出される or 残り時間が0になったら呼び出される。
    [StrixRpc]
    public void GameEnd()
    {
        if (!isEnded)
        {
            resultCanvas.SetActive(true);
            resultCanvas.GetComponent<ResultUIController>().SetActiveResultUICanvas(true, ScoreManager.instance.totalScore, NextPieceGenerator.instance.archiveValue);
            isEnded = true;
        }
    }
}
