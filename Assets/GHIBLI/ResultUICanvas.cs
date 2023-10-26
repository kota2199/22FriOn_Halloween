using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUICanvas : MonoBehaviour
{
    enum YokaiName{};
    string[] yokaiNames = { "ひとつめ小僧", "かっぱ", "ぬらりひょん", "座敷わらし", "提灯お化け", "ろくろ首", "アマビエ", "天狗" };

    [SerializeField] Sprite[] yokaiSprites;

    [SerializeField] Image resultYokaiImage;
    [SerializeField] Text resultScoreText;
    [SerializeField] Text resultYokaiNameText;

    [SerializeField] Text flavourText;

    string[] flavourtexts =
    {
        "お疲れさま！！",
        "よく頑張ったね！",
        "わっしょい！",
    };

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //テスト用
        if (Input.GetKeyDown(KeyCode.Space))
        {
            int score = Random.Range(0, 1000)*10;
            int yokaiIndex = Random.Range(0, 8);
            SetActiveResultUICanvas(!gameObject.GetComponent<Canvas>().enabled, score, yokaiIndex);
        }
    }

    //ゲームクリア表示をするときにこのメソッドを呼んでください!
    //引数1 value：UI(Canvas)のCanvasコンポーネントのSetActive(value)
    //引数2 score：そのままスコアになります
    //引数3 yokaiIndex：0～7で指定してください. 妖怪の画像、妖怪名を指定するのに配列のindexとして使います
    ////0が一つ目小僧で、7が天狗です
    public void SetActiveResultUICanvas(bool value,int score,int yokaiIndex)
    {
        gameObject.GetComponent<Canvas>().enabled = value;

        resultYokaiImage.sprite = yokaiSprites[yokaiIndex];
        resultScoreText.text = score.ToString();

        resultYokaiNameText.text = yokaiNames[yokaiIndex];

        flavourText.text = flavourtexts[Random.Range(0, flavourtexts.Length)];
    }
}
