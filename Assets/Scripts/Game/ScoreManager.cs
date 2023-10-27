using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoftGear.Strix.Unity.Runtime;


public class ScoreManager : StrixBehaviour
{
    public static ScoreManager instance;

    [StrixSyncField]
    public int totalScore = 0;

    [SerializeField]
    Text scoreText;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!isLocal)
        {
            return;
        }
        scoreText.text = "Score :" + totalScore;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddScore(int score)
    {
        Debug.Log("CalledS");
        RpcToAll(nameof(AddScoreEveryOne), score);
    }

    [StrixRpc]
    public void AddScoreEveryOne(int addScore)
    {
        Debug.Log("AddScore");
        totalScore += addScore;
        scoreText.text = "Score" + totalScore;
    }
}
