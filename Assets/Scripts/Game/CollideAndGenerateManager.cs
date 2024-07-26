using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Unity.Runtime;

public class CollideAndGenerateManager : StrixBehaviour
{
    public static CollideAndGenerateManager instance;

    private GameObject hitObj1, hitObj2;

    private GameObject genePiece;

    private Vector3 genePos;

    [SerializeField]
    private int[] scoreAmount;

    [SerializeField]
    private GameObject[] Pieces;

    [StrixSyncField]
    public bool isSimulated = false;

    //どの大きさのピースまで作ることが出来たか
    [StrixSyncField]
    public int archiveValue = 0;

    private ScoreManager scoreManager;

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
        scoreManager = GameObject.Find("GameManager").GetComponent<ScoreManager>();
    }

    //ピースが接触したときに呼び出される関数。引数は接触した相手のオブジェクト、自身のオブジェクト、ピースの種類
    public void CollideNotice(GameObject hitObj, GameObject selfObj, CollideManager.TypeOfPiece type)
    {
        if (!isLocal)
        {
            return;
        }

        //接触したピースが消えたときにピースを生成する座標(2つのピースの間)
        genePos = (hitObj.transform.position + selfObj.transform.position) / 2f;

        //ピースの種類を引数にして次のピースを生成する関数を呼び出す
        GenerateNextPiece(type);
    }

    //ピースの種類によって次に生成するピースを設定する
    void GenerateNextPiece(CollideManager.TypeOfPiece oldType)
    {
        switch (oldType)
        {
            case CollideManager.TypeOfPiece.Type1:
                scoreManager.AddScore(scoreAmount[0]);
                if (archiveValue <= 1)
                {
                    archiveValue = 1;
                }
                genePiece = Pieces[1];
                Generate();
                break;

            case CollideManager.TypeOfPiece.Type2:
                scoreManager.AddScore(scoreAmount[1]);
                if (archiveValue <= 2)
                {
                    archiveValue = 2;
                }
                genePiece = Pieces[2];
                Generate();
                break;

            case CollideManager.TypeOfPiece.Type3:
                scoreManager.AddScore(scoreAmount[2]);
                if (archiveValue <= 3)
                {
                    archiveValue = 3;
                }
                genePiece = Pieces[3];
                Generate();
                break;

            case CollideManager.TypeOfPiece.Type4:
                scoreManager.AddScore(scoreAmount[3]);
                if (archiveValue <= 4)
                {
                    archiveValue = 4;
                }
                genePiece = Pieces[4];
                Generate();
                break;
            case CollideManager.TypeOfPiece.Type5:
                scoreManager.AddScore(scoreAmount[4]);
                if (archiveValue <= 5)
                {
                    archiveValue = 5;
                }
                genePiece = Pieces[5];
                Generate();
                break;
            case CollideManager.TypeOfPiece.Type6:
                scoreManager.AddScore(scoreAmount[5]);
                if (archiveValue <= 6)
                {
                    archiveValue = 6;
                }
                genePiece = Pieces[6];
                Generate();
                break;
            case CollideManager.TypeOfPiece.Type7:
                scoreManager.AddScore(scoreAmount[6]);

                if (archiveValue <= 7)
                {
                    archiveValue = 7;
                }
                genePiece = Pieces[7];
                Generate();
                break;

            case CollideManager.TypeOfPiece.Type8:
                scoreManager.AddScore(scoreAmount[7]);
                break;
        }
        AudioController.Instance.PlaySe(3);
        isSimulated = true;
    }
    
    //次のピースを生成する
    void Generate()
    {
        GameObject piece = Instantiate(genePiece, genePos, Quaternion.identity);
        CollideManager collideManager = piece.GetComponent<CollideManager>();

        //Rigidbodyの計算を開始する
        RpcToAll(nameof(collideManager.Simulate));
    }
}
