using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using SoftGear.Strix.Unity.Runtime;

public class ResultUICanvas : StrixBehaviour
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

        // Update is called once per frame
        void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int score = Random.Range(0, 1000)*10;
            int yokaiIndex = Random.Range(0, 8);
            SetActiveResultUICanvas(!gameObject.GetComponent<Canvas>().enabled, score, yokaiIndex);
        }
    }

    public void SetActiveResultUICanvas(bool value,int score,int yokaiIndex)
    {
        gameObject.GetComponent<Canvas>().enabled = value;
        
        resultScoreText.text = score.ToString();

        resultYokaiImage.sprite = yokaiSprites[yokaiIndex];
        resultYokaiNameText.text = yokaiNames[yokaiIndex];

        this.yokaiIndex = yokaiIndex;
    }

    public void ReturnToTitle()
    {
        strixNetwork.DisconnectMasterServer();
        strixNetwork.roomSession.Disconnect();
        SceneManager.LoadScene("InGame");
    }
}
