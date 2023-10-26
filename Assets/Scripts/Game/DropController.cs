using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Unity.Runtime;

public class DropController : StrixBehaviour
{
    public static DropController instance;

    bool isMovingRight = false, isMovingLeft = false;

    public GameObject[] Pieces;

    public GameObject SetPiece;

    [SerializeField]
    GameObject dropButton;

    bool dropped = true;

    // Update is called once per frame
    void Update()
    {
        if (!isLocal)
        {
            return;
        }
        float currentPos = transform.position.x;
        if (isMovingRight && currentPos < 3.5f && !dropped)
        {
            transform.position += new Vector3(2f, 0, 0) * Time.deltaTime;
        }
        else if(isMovingLeft && currentPos > -3 && !dropped)
        {
            transform.position -= new Vector3(2f, 0, 0) * Time.deltaTime;
        }
    }

    public void PushRightButton()
    {
        if (!isLocal)
        {
            return;
        }
        isMovingRight = true;
    }

    public void ReleaseRightButton()
    {
        if (!isLocal)
        {
            return;
        }
        isMovingRight = false;
    }
    public void PushLeftButton()
    {
        if (!isLocal)
        {
            return;
        }
        isMovingLeft = true;
    }

    public void ReleaseLeftButton()
    {
        if (!isLocal)
        {
            return;
        }
        isMovingLeft = false;
    }
    public void GenerateForWait()
    {
        if (!isLocal)
        {
            return;
        }
        Debug.Log("GeneratePiece");
        RpcToAll(nameof(GeneratePiece));
    }
    [StrixRpc]
    public void GeneratePiece()
    {
        Debug.Log("Instantiate");
        SetPiece = Instantiate(PieceSet(), transform.position, Quaternion.identity);
        dropped = false;
        if (!isLocal)
        {
            return;
        }

        Debug.Log("SetParent");
        SetPiece.transform.SetParent(gameObject.transform);
        dropButton.SetActive(true);
    }
    private GameObject PieceSet()
    {
        if (!isLocal)
        {
            return null;
        }
        Debug.Log("PieceSet");
        int randomeNum = Random.Range(0, 5);
        GameObject piece = Pieces[randomeNum];
        return piece;
    }
    public void DropPiece()
    {
        Debug.Log("DropPiece");
        RpcToAll(nameof(Drop));
        if (!isLocal)
        {
            return;
        }
    }

    [StrixRpc]
    public void Drop()
    {
        if (!isLocal)
        {
            return;
        }
        Debug.Log("Drop");
        SetPiece.GetComponent<CollideManager>().SimulateOn();
        SetPiece.transform.parent = null;
        dropped = true;
        Invoke("GenerateForWait", 3f);
    }
}