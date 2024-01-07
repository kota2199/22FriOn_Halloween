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
        scoreManager = GameObject.Find("ScoreManager").GetComponent<ScoreManager>();
    }
    public void CollideNotice(GameObject hitObj, GameObject selfObj, CollideManager.TypeOfPiece type)
    {
        if (!isLocal)
        {
            return;
        }

        genePos = (hitObj.transform.position + selfObj.transform.position) / 2f;
        GenerateNextPiece(type);
    }

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

    void Generate()
    {
        GameObject piece = Instantiate(genePiece, genePos, Quaternion.identity);
        CollideManager collideManager = piece.GetComponent<CollideManager>();
        RpcToAll(nameof(collideManager.Simulate));
    }
}
