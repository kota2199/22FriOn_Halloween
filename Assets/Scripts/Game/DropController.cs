using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoftGear.Strix.Unity.Runtime;

public class DropController : StrixBehaviour
{
    public static DropController instance;

    public GameObject[] Pieces;

    public GameObject[] PiecesForAfterDrop;

    public GameObject SetPiece;

    [SerializeField]
    private GameObject dropButton;

    private bool dropped = true;

    [SerializeField]
    private Text playerNameText;

    [StrixSyncField]
    private string playerName;

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
        GeneratePiece();
    }
    [StrixRpc]
    public void Displayname()
    {
        playerName = this.gameObject.GetComponent<StrixReplicator>().roomMember.GetName();
        playerNameText.text = playerName;
    }
    [StrixRpc]
    public void GeneratePiece()
    {
        SetPiece = Instantiate(SetRandomPiece(), transform.position, Quaternion.identity);
        dropButton.SetActive(true);
    }
    private GameObject SetRandomPiece()
    {
        int randomeNum = Random.Range(0, 3);
        GameObject piece = Pieces[randomeNum];
        return piece;
    }
    public void DropPiece()
    {
        if(SetPiece != null)
        {
            AudioController.Instance.PlaySe(2);
            Vector3 dropPos = new Vector3(SetPiece.transform.position.x, SetPiece.transform.position.y, SetPiece.transform.position.z);
            CollideManager.TypeOfPiece type = SetPiece.GetComponent<CollideManager>().typeOfPiece;

            switch (type)
            {
                case CollideManager.TypeOfPiece.Type1:
                    RpcToRoomOwner(nameof(Drop), 0, dropPos);
                    break;
                case CollideManager.TypeOfPiece.Type2:
                    RpcToRoomOwner(nameof(Drop), 1, dropPos);
                    break;
                case CollideManager.TypeOfPiece.Type3:
                    RpcToRoomOwner(nameof(Drop), 2, dropPos);
                    break;
            }
            Invoke("WaitGanerate", 3f);
            Destroy(SetPiece);
        }
    }

    private void WaitGanerate()
    {
        GeneratePiece();
    }

    [StrixRpc]
    public void Drop(int pieceForRegene, Vector3 PosForRegene)
    {
        GameObject droppedObject = Instantiate(PiecesForAfterDrop[pieceForRegene], PosForRegene, Quaternion.identity);
        droppedObject.GetComponent<CollideManager>().Simulate();
        dropped = true;
    }
}