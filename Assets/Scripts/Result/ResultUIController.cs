using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SoftGear.Strix.Unity.Runtime;

public class ResultUIController : StrixBehaviour
{
    enum YokaiName{};
    string[] yokaiNames = { "一つ目小僧", "かっぱ", "ぬらりひょん", "ちょうちん", "座敷わらし", "ろくろ首", "アマビエ", "天狗" };

    [SerializeField] Sprite[] yokaiSprites;

    [SerializeField] Text resultText;

    [SerializeField] Image resultYokaiImage;
    [SerializeField] Text resultScoreText;
    [SerializeField] Text resultYokaiNameText;

    int yokaiIndex;
    StrixNetwork strixNetwork;

    private void Start()
    {
        strixNetwork = StrixNetwork.instance;

    }

    //引数：リザルト画面の表示ステータス(bool)、スコア、到達できた最大の妖怪のインデックス
    public void SetActiveResultUICanvas(bool value,int score,int yokaiIndex)
    {
        gameObject.GetComponent<Canvas>().enabled = value;
        
        resultScoreText.text = score.ToString();

        resultYokaiImage.sprite = yokaiSprites[yokaiIndex];
        resultYokaiNameText.text = yokaiNames[yokaiIndex];

        this.yokaiIndex = yokaiIndex;
    }

    //タイトルへ戻るボタン
    public void ReturnToTitle()
    {
        //AudioController.Instance.PlaySe(0);
        strixNetwork.DisconnectMasterServer();
        strixNetwork.roomSession.Disconnect();
        AudioController.Instance.ChangeSceneWithSE(0, 0);
        //SceneManager.LoadScene("InGame");
    }
}
