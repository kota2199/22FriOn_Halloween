using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Unity.Runtime;

public class DropController : StrixBehaviour
{
    public static DropController instance;

    bool isMovingRight = false, isMovingLeft = false;

    public GameObject[] Pieces;

    [StrixSyncField]
    public GameObject SetPiece = null;

    [SerializeField]
    GameObject dropButton;

    bool dropped = true;

    [StrixSyncField]
    public bool isSimulate = false;
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
    void Start()
    {
        //GeneratePiece();
    }

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
        RpcToAll(nameof(GeneratePiece));
    }
    [StrixRpc]
    public void GeneratePiece()
    {
        if (!isLocal)
        {
            return;
        }
            SetPiece = Instantiate(PieceSet(), transform.position, Quaternion.identity);
            SetPiece.transform.SetParent(gameObject.transform);
            dropped = false;
            dropButton.SetActive(true);
    }
    private GameObject PieceSet()
    {
        if (!isLocal)
        {
            return null;
        }
        int randomeNum = Random.Range(0, 5);
        GameObject piece = Pieces[randomeNum];
        return piece;
    }
    public void DropPiece()
    {
        if (!isLocal)
        {
            return;
        }
        RpcToAll(nameof(Drop));
    }

    [StrixRpc]
    public void Drop()
    {
        isSimulate = true;
        //CollideManager collideManager = SetPiece.GetComponent<CollideManager>();
        //RpcToAll(nameof(collideManager.Simulate));
        SetPiece.GetComponent<Rigidbody2D>().simulated = isSimulate;
        SetPiece.transform.parent = null;
        dropped = true;
        dropButton.SetActive(false);
        Invoke("GenerateForWait", 3f);
    }
}