using SoftGear.Strix.Client.Core.Model.Manager.Filter;
using SoftGear.Strix.Client.Core.Model.Manager.Filter.Builder;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoftGear.Strix.Client.Match.Room.Model;
using SoftGear.Strix.Unity.Runtime;
using System.Linq;

public class ConnectToRoom : StrixBehaviour
{
    string playerName = "Player";
    string roomName = "Room_A";

    [SerializeField]
    Dropdown roomNameField;
    [SerializeField]
    Text playerNameField;

    [SerializeField]
    GameObject connectPanel, readyUI;

    StrixNetwork strixNetwork;

    // Start is called before the first frame update
    void Start()
    {
        strixNetwork = StrixNetwork.instance;

        // これは仮の値です。実際のアプリケーションIDに変更してください
        // Strix Cloudのアプリケーション情報タブにあります: https://www.strixcloud.net/app/applist
        strixNetwork.applicationId = "3de7734b-7f47-4673-bfb1-5415554765e4";

        // まずマスターサーバーに接続します
        strixNetwork.ConnectMasterServer(
            // これは仮の値です。実際のマスターホスト名に変更してください。
            // Strix Cloudのアプリケーション情報タブにあります: https://www.strixcloud.net/app/applist
            host: "32ae743d6ca4d45daf916f97.game.strixcloud.net",
            connectEventHandler: _ => {
                Debug.Log("Connection established.");
            },
            errorEventHandler: connectError => Debug.LogError("Connection failed. Reason: " + connectError.cause)
        );
    }
    public void ChangeRoom()
    {
        if (!isLocal)
        {
            return;
        }
        if (roomNameField.value == 0)
        {
            roomName = "Room_A";
        }
        else if (roomNameField.value == 1)
        {
            roomName = "Room_B";
        }
        else if (roomNameField.value == 2)
        {
            roomName = "Room_C";
        }
        Debug.Log(roomName);
    }

    public void SerchRoom()
    {
        playerName = playerNameField.text.ToString();

        strixNetwork.SearchRoom
            (
                condition: ConditionBuilder.Builder().Field("name").EqualTo(roomName).Build(),
                limit: 1,
                offset: 0,
                handler: searchResults => 
                {
                    Debug.Log(searchResults.roomInfoCollection.Count + " rooms found.");

                    foreach (var roomInfo in searchResults.roomInfoCollection)
                        JoinRoom(roomInfo.host, roomInfo.port, roomInfo.protocol, roomInfo.id);
                },
                failureHandler: searchError => Debug.Log("Search failed. Reason: " + searchError.cause)
            );
        CreatRoom();
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
                          }
                    );
                    Debug.Log("Room joined.");
                },
                failureHandler: joinError => Debug.LogError("Join failed. Reason: " + joinError.cause)
            );
    }
    public void CreatRoom()
    {
        strixNetwork.CreateRoom
            (
                new RoomProperties
                {
                    name = roomName,
                   capacity = 10,
                },
                new RoomMemberProperties
                {
                    name = playerName,
                    properties = new Dictionary<string, object>()
                    {
                        { "state", 0 }  // 初期状態は "Not Ready"
                    }
                },
                handler: __ =>
                {
                    Debug.Log("Room created.");
                },
                failureHandler: createRoomError => Debug.LogError("Could not create room. Reason: " + createRoomError.cause)
            );
        

        connectPanel.SetActive(false);
        readyUI.SetActive(true);
    }

    public void Ready()
    {

    }
    

}
