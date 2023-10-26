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
            Destroy(this.gameObject);
        }
    }

    //Turn on Rigidbody2D's simulated.
    public void SimulateOn()
    {
        if (!isLocal)
        {
            return;
        }
        RpcToAll(nameof(Simulate));
    }
    [StrixRpc]
    public void Simulate()
    {
        isSimulated = true;
        GetComponent<Rigidbody2D>().simulated = isSimulated;
    }
}
