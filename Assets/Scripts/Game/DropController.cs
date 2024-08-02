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

        //プレイヤーがピースを持っていたら、プレイヤーの横移動に合わせてピースを移動させる
        if (SetPiece != null)
        {
            SetPiece.transform.position = transform.position;
        }
    }

    //プレイヤーがルームに参加したらプレイヤーネームを表示させ、最初のピースを生成する
    public void RoomJoined()
    {
        RpcToAll(nameof(Displayname));

        if (!isLocal)
        {
            return;
        }
        GeneratePiece();
    }

    //プレイヤーネームを表示
    [StrixRpc]
    public void Displayname()
    {
        playerName = this.gameObject.GetComponent<StrixReplicator>().roomMember.GetName();
        playerNameText.text = playerName;
    }

    //ピースを生成し、プレイヤーに持たせる
    public void GeneratePiece()
    {
        SetPiece = Instantiate(SetRandomPiece(), transform.position, Quaternion.identity);
        dropButton.SetActive(true);
    }

    //Type1~3のピースを生成するためのランダムにインデックスを指定する
    private GameObject SetRandomPiece()
    {
        int randomeNum = Random.Range(0, 3);
        GameObject piece = Pieces[randomeNum];
        return piece;
    }

    //落とすピースをセットする
    public void SetDropPiece()
    {
        if(SetPiece != null)
        {
            AudioController.Instance.PlaySe(2);

            //プレイヤーが持っている表示用のピースの位置から、盤面に落とすピースを生成するための座標を代入
            Vector3 dropPos = new Vector3(SetPiece.transform.position.x, SetPiece.transform.position.y, SetPiece.transform.position.z);

            //プレイヤーが持っているピースの種類を取得する
            CollideNotificator.TypeOfPiece type = SetPiece.GetComponent<CollideNotificator>().typeOfPiece;

            //タイプ別にピースをドロップする
            switch (type)
            {
                case CollideNotificator.TypeOfPiece.Type1:
                    RpcToRoomOwner(nameof(Drop), 0, dropPos);
                    break;
                case CollideNotificator.TypeOfPiece.Type2:
                    RpcToRoomOwner(nameof(Drop), 1, dropPos);
                    break;
                case CollideNotificator.TypeOfPiece.Type3:
                    RpcToRoomOwner(nameof(Drop), 2, dropPos);
                    break;
            }
            //Destroy piece
            Destroy(SetPiece);

            //3秒後に生成する
            Invoke("ReadyForGenerate", 3f);
        }
    }

    //プレイヤーに次のピースをセットするまで3秒まつ
    void ReadyForGenerate()
    {
        if (!isLocal)
        {
            return;
        }
        GeneratePiece();
    }

    //ピースをドロップする
    [StrixRpc]
    public void Drop(int pieceForRegene, Vector3 PosForRegene)
    {
        GameObject droppedObject = Instantiate(PiecesForAfterDrop[pieceForRegene], PosForRegene, Quaternion.identity);

        //Rigidbodyのsimulateを開始する
        droppedObject.GetComponent<CollideNotificator>().Simulate();

        dropped = true;
    }
}