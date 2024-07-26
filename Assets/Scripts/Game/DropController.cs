using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoftGear.Strix.Unity.Runtime;

public class DropController : StrixBehaviour
{
    //Piece before drop
    public GameObject[] Pieces;

    //Piece after drop
    public GameObject[] PiecesForAfterDrop;

    //Player's piece
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

        //If player has the piece, match the piece position with the player position.
        if (SetPiece != null)
        {
            SetPiece.transform.position = transform.position;
        }
    }

    //When player join the room, display player name and generate first piece.
    public void RoomJoined()
    {
        RpcToAll(nameof(Displayname));

        if (!isLocal)
        {
            return;
        }
        GeneratePiece();
    }

    //Display player name
    [StrixRpc]
    public void Displayname()
    {
        playerName = this.gameObject.GetComponent<StrixReplicator>().roomMember.GetName();
        playerNameText.text = playerName;
    }

    //Generate piece
    public void GeneratePiece()
    {
        SetPiece = Instantiate(SetRandomPiece(), transform.position, Quaternion.identity);
        dropButton.SetActive(true);
    }

    //Set the object to generate either the first, second, or third piece from the smallest
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

            //Assign the coordinates of the piece before it was dropped to the coordinates of the piece that produced the dropped piece.
            Vector3 dropPos = new Vector3(SetPiece.transform.position.x, SetPiece.transform.position.y, SetPiece.transform.position.z);

            //Get piece type.
            CollideManager.TypeOfPiece type = SetPiece.GetComponent<CollideManager>().typeOfPiece;

            //For each type of piece, select a piece from the piece array and pass it to the Drop function with the piece to be dropped and its coordinates as arguments.
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
            //Destroy piece
            Destroy(SetPiece);

            //Generate after 3 seconds
            Invoke("ReadyForGenerate", 3f);
        }
    }

    void ReadyForGenerate()
    {
        if (!isLocal)
        {
            return;
        }
        GeneratePiece();
    }

    //Drop piece
    [StrixRpc]
    public void Drop(int pieceForRegene, Vector3 PosForRegene)
    {
        //When each player has a piece, each user is the owner of the object, but once the piece is dropped, the room owner becomes the owner of the object.
        GameObject droppedObject = Instantiate(PiecesForAfterDrop[pieceForRegene], PosForRegene, Quaternion.identity);

        //Start Rigidbody simulate
        droppedObject.GetComponent<CollideManager>().Simulate();

        dropped = true;
    }
}