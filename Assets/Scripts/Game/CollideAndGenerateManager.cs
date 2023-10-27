using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Unity.Runtime;

public class CollideAndGenerateManager : StrixBehaviour
{
    public static CollideAndGenerateManager instance;

    GameObject hitObj1, hitObj2;

    GameObject genePiece;

    Vector3 genePos;

    [SerializeField]
    int[] scoreAmount;

    [SerializeField]
    GameObject[] Pieces;

    [StrixSyncField]
    public bool isSimulated = false;

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
    private void Update()
    {
        Debug.Log(ScoreManager.instance);
    }

    public void CollideNotice(GameObject hitObj, GameObject selfObj, CollideManager.TypeOfPiece type)
    {
        if (!isLocal)
        {
            return;
        }
        if(hitObj == hitObj2 && selfObj == hitObj1)
        {
            genePos = (hitObj1.transform.position + hitObj2.transform.position) / 2f;
            //RpcToAll(nameof(GenerateNextPiece), type);
            GenerateNextPiece(type);
        }
        else
        {
            hitObj1 = hitObj;
            hitObj2 = selfObj;
        }
    }

   // [StrixRpc]
    void GenerateNextPiece(CollideManager.TypeOfPiece oldType)
    {
        Debug.Log("GenerateNextPiece");
        switch (oldType)
        {
            case CollideManager.TypeOfPiece.Type1:
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().AddScore(scoreAmount[0]);
                //ScoreManager.instance.AddScore(scoreAmount[0]);
                genePiece = Pieces[1];
                break;
            case CollideManager.TypeOfPiece.Type2:
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().AddScore(scoreAmount[1]);
                //ScoreManager.instance.AddScore(scoreAmount[1]);
                genePiece = Pieces[2];
                break;
            case CollideManager.TypeOfPiece.Type3:
                GameObject.Find("ScoreManager").GetComponent<ScoreManager>().AddScore(scoreAmount[1]);
                //ScoreManager.instance.AddScore(scoreAmount[2]);
                genePiece = Pieces[3];
                break;
            case CollideManager.TypeOfPiece.Type4:
                ScoreManager.instance.AddScore(scoreAmount[3]);
                genePiece = Pieces[4];
                break;
            case CollideManager.TypeOfPiece.Type5:
                ScoreManager.instance.AddScore(scoreAmount[4]);
                genePiece = Pieces[5];
                break;
            case CollideManager.TypeOfPiece.Type6:
                ScoreManager.instance.AddScore(scoreAmount[5]);
                genePiece = Pieces[6];
                break;
            case CollideManager.TypeOfPiece.Type7:
                ScoreManager.instance.AddScore(scoreAmount[6]);
                genePiece = Pieces[7];
                break;
            case CollideManager.TypeOfPiece.Type8:
                ScoreManager.instance.AddScore(scoreAmount[7]);
                genePiece = Pieces[8];
                break;
            case CollideManager.TypeOfPiece.Type9:
                ScoreManager.instance.AddScore(scoreAmount[8]);
                genePiece = Pieces[9];
                break;
            case CollideManager.TypeOfPiece.Type10:
                ScoreManager.instance.AddScore(scoreAmount[9]);
                genePiece = Pieces[10];
                break;
            case CollideManager.TypeOfPiece.Type11:
                ScoreManager.instance.AddScore(scoreAmount[10]);
                ScoreManager.instance.AddScore(scoreAmount[10]);
                break;
        }
        Debug.Log("NextPiece");
        SEController.Instance.PlaySe(3);
        isSimulated = true;
        Instantiate(genePiece, genePos, Quaternion.identity).GetComponent<Rigidbody2D>().simulated = isSimulated;
    }
}
