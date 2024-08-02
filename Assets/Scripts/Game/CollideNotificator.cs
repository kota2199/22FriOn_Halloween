using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoftGear.Strix.Unity.Runtime;

public class CollideNotificator : StrixBehaviour
{
    [StrixSyncField]
    public bool isSimulated = false;
    public enum TypeOfPiece
    {
        Type1, Type2, Type3, Type4, Type5, Type6, Type7, Type8, Type9, Type10, Type11
    }

    public TypeOfPiece typeOfPiece;

    private GameObject manager;

    [SerializeField]
    private GameObject resultCanvas;

    private void Start()
    {
        manager = GameObject.FindWithTag("Manager");
    }

    //同じタイプのピースと接触したとき
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CollideNotificator>()
            && collision.gameObject.GetComponent<CollideNotificator>().typeOfPiece == typeOfPiece)
        {
            //自身と接触相手のInstanceIDを比較し、自身のIDのほうが大きい場合NextPieceGeneratorに次のピースの生成処理を飛ばす
            int selfId = gameObject.GetInstanceID();
            int collisionId = collision.gameObject.GetInstanceID();

            if (selfId > collisionId)
            {
                manager.GetComponent<NextPieceGenerator>().CollideNotice
                    (this.gameObject, collision.gameObject, typeOfPiece);
            }

            //このピースを削除する
            RpcToAll(nameof(DestroyPiece));
        }
    }

    //天井にあるゲームオーバーのラインにピースが触れたらゲームオーバー。TimeManager.GameEndを呼び出す。
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GameOver")
        {
            TimeManager.instance.RpcToAll(nameof(TimeManager.instance.GameEnd));
        }
    }

    //このピースを削除する
    [StrixRpc]
    void DestroyPiece()
    {
        Destroy(gameObject);
    }

    //ピースのRigidbody2DのSimulatedを有効化
    [StrixRpc]
    public void Simulate()
    {
        isSimulated = true;
        GetComponent<Rigidbody2D>().simulated = isSimulated;
    }
}
