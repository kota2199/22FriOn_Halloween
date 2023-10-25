using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideAndGenerateManager : MonoBehaviour
{
    public static CollideAndGenerateManager instance;

    GameObject hitObj1, hitObj2;

    GameObject genePiece;

    Vector3 genePos;

    [SerializeField]
    int[] scoreAmount;

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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CollideNotice(GameObject hitObj, GameObject selfObj, CollideManager.TypeOfPiece type)
    {
        if(hitObj == hitObj2 && selfObj == hitObj1)
        {
            genePos = (hitObj1.transform.position + hitObj2.transform.position) / 2f;
            GenerateNextPiece(type);
        }
        else
        {
            hitObj1 = hitObj;
            hitObj2 = selfObj;
        }
    }

    void GenerateNextPiece(CollideManager.TypeOfPiece oldType)
    {
        switch (oldType)
        {
            case CollideManager.TypeOfPiece.Type1:
                ScoreManager.instance.AddScore(scoreAmount[0]);
                genePiece = DropController.instance.Pieces[1];
                break;
            case CollideManager.TypeOfPiece.Type2:
                ScoreManager.instance.AddScore(scoreAmount[1]);
                genePiece = DropController.instance.Pieces[2];
                break;
            case CollideManager.TypeOfPiece.Type3:
                ScoreManager.instance.AddScore(scoreAmount[2]);
                genePiece = DropController.instance.Pieces[3];
                break;
            case CollideManager.TypeOfPiece.Type4:
                ScoreManager.instance.AddScore(scoreAmount[3]);
                genePiece = DropController.instance.Pieces[4];
                break;
            case CollideManager.TypeOfPiece.Type5:
                ScoreManager.instance.AddScore(scoreAmount[4]);
                genePiece = DropController.instance.Pieces[5];
                break;
            case CollideManager.TypeOfPiece.Type6:
                ScoreManager.instance.AddScore(scoreAmount[5]);
                genePiece = DropController.instance.Pieces[6];
                break;
            case CollideManager.TypeOfPiece.Type7:
                ScoreManager.instance.AddScore(scoreAmount[6]);
                genePiece = DropController.instance.Pieces[7];
                break;
            case CollideManager.TypeOfPiece.Type8:
                ScoreManager.instance.AddScore(scoreAmount[7]);
                genePiece = DropController.instance.Pieces[8];
                break;
            case CollideManager.TypeOfPiece.Type9:
                ScoreManager.instance.AddScore(scoreAmount[8]);
                genePiece = DropController.instance.Pieces[9];
                break;
            case CollideManager.TypeOfPiece.Type10:
                ScoreManager.instance.AddScore(scoreAmount[9]);
                genePiece = DropController.instance.Pieces[10];
                break;
            case CollideManager.TypeOfPiece.Type11:
                ScoreManager.instance.AddScore(scoreAmount[10]);
                Debug.Log("Delete");
                break;
        }
        Instantiate(genePiece, genePos, Quaternion.identity).GetComponent<Rigidbody2D>().simulated = true;
    }
}
