using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoftGear.Strix.Unity.Runtime;

public class DropController : StrixBehaviour
{
    public static DropController instance;

    bool isMovingRight = false, isMovingLeft = false;

    public GameObject[] Pieces;

    public GameObject[] PiecesForAfterDrop;

    public GameObject SetPiece;

    [SerializeField]
    GameObject dropButton;

    bool dropped = true;

    [SerializeField]
    Text playerNameText;

    [StrixSyncField]
    string playerName;

    StrixNetwork strixNetwork;
    private void Start()
    {
        strixNetwork = StrixNetwork.instance;
    }
    // Update is called once per frame
    void Update()
    {
        if (!isLocal)
        {
            return;
        }
        if (SetPiece != null)
        {
            SetPiece.transform.position = transform.position;
        }
    }
    public void RoomJoined()
    {
        RpcToAll(nameof(Displayname));
    }
    [StrixRpc]
    public void Displayname()
    {
        playerName = this.gameObject.GetComponent<StrixReplicator>().roomMember.GetName();
        playerNameText.text = playerName;
    }
    public void GenerateForWait()
    {
        //RpcToAll(nameof(GeneratePiece));
        GeneratePiece();
        Debug.Log("GeneratePiece");
        dropButton.SetActive(true);
    }
    //[StrixRpc]
    public void GeneratePiece()
    {
        Debug.Log("Instantiate");
        SetPiece = Instantiate(PieceSet(), transform.position, Quaternion.identity);
    }
    private GameObject PieceSet()
    {
        Debug.Log("PieceSet");
        int randomeNum = Random.Range(0, 3);
        GameObject piece = Pieces[randomeNum];
        return piece;
        if (!isLocal)
        {
            return null;
        }
    }
    public void DropPiece()
    {
        if(SetPiece != null)
        {
            SEController.Instance.PlaySe(2);
            Debug.Log("DropPiece");
            Vector3 dropPos = new Vector3(SetPiece.transform.position.x, SetPiece.transform.position.y, SetPiece.transform.position.z);
            CollideManager.TypeOfPiece type = SetPiece.GetComponent<CollideManager>().typeOfPiece;

            switch (type)
            {
                case CollideManager.TypeOfPiece.Type1:
                    Debug.Log("Type1");
                    RpcToRoomOwner(nameof(Drop), 0, dropPos);
                    break;
                case CollideManager.TypeOfPiece.Type2:
                    Debug.Log("Type2");
                    //ScoreManager.instance.AddScore(scoreAmount[1]);
                    RpcToRoomOwner(nameof(Drop), 1, dropPos);
                    break;
                case CollideManager.TypeOfPiece.Type3:
                    Debug.Log("Type3");
                    //ScoreManager.instance.AddScore(scoreAmount[2]);
                    RpcToRoomOwner(nameof(Drop), 2, dropPos);
                    break;
            }
            Invoke("GenerateForWait", 3f);
            Destroy(SetPiece);
        }
    }

    [StrixRpc]
    public void Drop(int pieceForRegene, Vector3 PosForRegene)
    {
        Debug.Log("Drop");
        GameObject droppedObject = Instantiate(PiecesForAfterDrop[pieceForRegene], PosForRegene, Quaternion.identity);
        droppedObject.GetComponent<CollideManager>().Simulate();
        Debug.Log("Dropped");
        dropped = true;
        //Invoke("GenerateForWait", 3f);
    }
}