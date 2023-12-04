using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoftGear.Strix.Unity.Runtime;

public class CollideManager : StrixBehaviour
{
    [StrixSyncField]
    public bool isSimulated = false;
    public enum TypeOfPiece
    {
        Type1, Type2, Type3, Type4, Type5, Type6, Type7, Type8, Type9, Type10, Type11
    }

    public TypeOfPiece typeOfPiece;

    GameObject manager;

    bool isEnded = false;
    private void Start()
    {
        manager = GameObject.FindWithTag("Manager");
    }
    //接触した二つのピースのうち、IDが小さいほうに次のピースを生成する処理を行わせる。
    private void OnCollisionEnter2D(Collision2D collision)
    {
        int selfId = gameObject.GetInstanceID();
        int collisionId = collision.gameObject.GetInstanceID();

        if (collision.gameObject.GetComponent<CollideManager>()
            && collision.gameObject.GetComponent<CollideManager>().typeOfPiece == typeOfPiece)
        {
            if (selfId > collisionId)
            {
                manager.GetComponent<CollideAndGenerateManager>().CollideNotice
                    (this.gameObject, collision.gameObject, typeOfPiece);
            }
            Destroy(this.gameObject);
            RpcToAll(nameof(DestroyPiece));
        }
    }
    
    //GameOverタグを持つオブジェクトと接触したらゲームオーバー
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GameOver")
        {
            RpcToAll(nameof(GameEnd));
        }
    }

    [StrixRpc]
    void DestroyPiece()
    {
        Destroy(gameObject);
    }
    [StrixRpc]
    public void Simulate()
    {
        isSimulated = true;
        GetComponent<Rigidbody2D>().simulated = isSimulated;
    }
    [StrixRpc]
    public void GameEnd()
    {
        if (!isEnded)
        {
            GameObject.Find("ReslutUICanvas").GetComponent<ResultUICanvas>().SetActiveResultUICanvas(true, ScoreManager.instance.totalScore, CollideAndGenerateManager.instance.archiveValue, false);
            isEnded = true;
        }
    }
}
