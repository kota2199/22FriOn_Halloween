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

    [SerializeField]
    GameObject gameOverCanvas;

    private void Start()
    {
        manager = GameObject.FindWithTag("Manager");
        Debug.Log(manager);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CollideManager>()
            && collision.gameObject.GetComponent<CollideManager>().typeOfPiece == typeOfPiece)
        {
            manager.GetComponent<CollideAndGenerateManager>().CollideNotice
                (this.gameObject, collision.gameObject, typeOfPiece);
            //Destroy(this.gameObject);
            RpcToAll(nameof(DestroyPiece));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GameOver")
        {
            Debug.Log("GameOver");
            gameOverCanvas.SetActive(true);
        }
    }
    [StrixRpc]
    void DestroyPiece()
    {
        Destroy(gameObject);
    }

    //Turn on Rigidbody2D's simulated.
    /*public void SimulateOn()
    {
        RpcToRoomOwner(nameof(Simulate));
    }
    */
    [StrixRpc]
    public void Simulate()
    {
        Debug.Log("Owner");
        isSimulated = true;
        GetComponent<Rigidbody2D>().simulated = isSimulated;
    }
}
