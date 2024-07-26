using SoftGear.Strix.Client.Core.Model.Manager.Filter;
using SoftGear.Strix.Client.Core.Model.Manager.Filter.Builder;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using SoftGear.Strix.Client.Match.Room.Model;
using SoftGear.Strix.Unity.Runtime;
using System.Linq;

public class ConnectToRoom : StrixBehaviour
{
    /// <summary>
    /// Data
    /// </summary>
    [SerializeField]
    private StrixAppData appData;

    private string playerName = "Player";

    private string webGLHostUrl;

    /// <summary>
    /// Room Create
    /// </summary>
    [SerializeField]
    private Text roomNameField;

    [SerializeField]
    private Text createRoomErrorTxt;

    /// <summary>
    /// SearchRoom
    /// </summary>
    [SerializeField]
    private GameObject roomInfoObj;

    [SerializeField]
    private GameObject roomInfoParent;

    /// <summary>
    /// Register PlayerName
    /// </summary>
    [SerializeField]
    private Text playerNameField;

    [SerializeField]
    private List<GameObject> roomList = new List<GameObject>();

    /// <summary>
    /// UIs
    /// </summary>
    private SwitchMenuUI uiSwitcher;

    [SerializeField]
    private GameObject connectPanel, readyUI;

    [SerializeField]
    private CautionPanelController cautionController;

    [SerializeField]
    private string failRoomEnter, failRoomCreate, faileRoomSearch, othersError;

    private ReadyStatusManager readyManager;

    //instance
    private StrixNetwork strixNetwork;

    private void Awake()
    {
        readyManager = GetComponent<ReadyStatusManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        uiSwitcher = GetComponent<SwitchMenuUI>();

        //Instance作成
        if (strixNetwork == null)
        {
            strixNetwork = StrixNetwork.instance;
        }

        //Combine URL for WebGL
        webGLHostUrl = "wss://" + appData.hostUrl + ":9122";

        //Get applicationID
        strixNetwork.applicationId = appData.applicationID;

        //Connect
        strixNetwork.ConnectMasterServer(webGLHostUrl,
            connectEventHandler: _ => {
                Debug.Log("Connection established.");
            },
            errorEventHandler: connectError => Debug.LogError("Connection failed. Reason: " + connectError.cause)
        );
    }

    public void SetPlayerName()
    {
        playerName = playerNameField.text;
        SwitchMenuUI switchMenuUI = GetComponent<SwitchMenuUI>();
        switchMenuUI.ToSerchRoom(switchMenuUI.setPlayerName);

        SerchRoom();
    }

    public void SerchRoom()
    {
        ResetRoomList();

        StrixNetwork.instance.SearchJoinableRoom(100, 0,
            args => {
                foreach (var roomInfo in args.roomInfoCollection)
                {
                    int listCount = 0;
                    string roomName = roomInfo.name;
                    int roomMember = roomInfo.memberCount;
                    
                    GameObject roomInfoList = Instantiate(roomInfoObj
                        , transform.position
                        , Quaternion.identity);

                    roomList.Add(roomInfoList);

                    roomInfoList.transform.SetParent(roomInfoParent.transform);

                    Vector3 genePos = new Vector3(0, listCount * -120, 0);
                    roomInfoList.GetComponent<RectTransform>().anchoredPosition = genePos;

                    roomInfoList.GetComponent<RectTransform>().localScale = new Vector3(1, 1, 1);

                    roomInfoList.transform.Find("background/t_RoomName").gameObject.GetComponent<Text>().text = roomName;
                    roomInfoList.transform.Find("background/t_NumberOfMember").gameObject.GetComponent<Text>().text = roomMember.ToString() + "/6";

                    JoinRoomBtnController joinRoomBtnController = roomInfoList.GetComponent<JoinRoomBtnController>();
                    joinRoomBtnController.roomConnector = this;
                    joinRoomBtnController.roomInfo = roomInfo;

                    listCount++;
                }
            },
            args => {
                Debug.Log("SearchJoinableRoom failed. error = " + args.cause);
                cautionController.DisplayCaution(faileRoomSearch);
            }
        );
    }

    private void ResetRoomList()
    {
        roomList.Clear();
    }

    public void JoinRoom(string host, int port, string protocol, long id)
    {
        strixNetwork.JoinRoom
            (
                host: host, 
                port: port,
                protocol: protocol,
                roomId: id,
                playerName: playerName,
                handler: __ =>
                {
                    strixNetwork.SetRoomMember
                    (
                        strixNetwork.selfRoomMember.GetPrimaryKey(),
                        new Dictionary<string, object>()
                        {
                            { "properties", new Dictionary<string, object>()
                                {
                                    { "state", 0 }
                                }
                            }
                        },
                        args =>
                          {
                              Debug.Log("SetRoomMember succeeded");
                          },
                          args => {
                              Debug.Log("SetRoomMember failed. error = " + args.cause);
                              cautionController.DisplayCaution(failRoomEnter);
                          }
                    );
                    Debug.Log("Room joined.");

                    uiSwitcher.ToReady(uiSwitcher.serchRoom);

                },
                failureHandler: joinError => {
                    Debug.LogError("Join failed. Reason: " + joinError.cause);
                    cautionController.DisplayCaution(failRoomEnter);
                }
            );
    }

    public void CreateRoom()
    {
        if(roomNameField.text == "")
        {
            createRoomErrorTxt.gameObject.SetActive(true);
            createRoomErrorTxt.text = "ルーム名を入力してください。";
        }
        else
        {
            StrixNetwork.instance.CreateRoom(
                new RoomProperties
                {
                    name = roomNameField.text,
                    capacity = 6,
                },
                new RoomMemberProperties
                {
                    name = playerName,
                    properties = new Dictionary<string, object>() {
                        { "state", 0 }
                    }
                },
                args => {
                    Debug.Log("CreateRoom succeeded");
                    uiSwitcher.ToReady(uiSwitcher.createRoom);
                },
                args => {
                    Debug.Log("CreateRoom failed. error = " + args.cause);
                    createRoomErrorTxt.gameObject.SetActive(true);
                    createRoomErrorTxt.text = "ルーム名を入力し直してください。";
                }
            );
        }
    }
}
