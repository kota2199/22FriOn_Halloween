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

    private GameObject manager;

    [SerializeField]
    private GameObject resultCanvas;

    private bool isEnded = false;
    private void Start()
    {
        manager = GameObject.FindWithTag("Manager");
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CollideManager>()
            && collision.gameObject.GetComponent<CollideManager>().typeOfPiece == typeOfPiece)
        {
            int selfId = gameObject.GetInstanceID();
            int collisionId = collision.gameObject.GetInstanceID();

            if (selfId > collisionId)
            {
                manager.GetComponent<CollideAndGenerateManager>().CollideNotice
                    (this.gameObject, collision.gameObject, typeOfPiece);
            }
            Destroy(this.gameObject);
            RpcToAll(nameof(DestroyPiece));
        }
    }
    
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
            resultCanvas.SetActive(true);
            resultCanvas.GetComponent<ResultUICanvas>().SetActiveResultUICanvas(true, ScoreManager.instance.totalScore, CollideAndGenerateManager.instance.archiveValue);
            isEnded = true;
        }
    }
}
