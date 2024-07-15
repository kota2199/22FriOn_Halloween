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
    [SerializeField]
    private StrixAppData appData;

    private string playerName = "Player";

    private string roomName = "Room_A";

    private string webGLHostUrl;

    [SerializeField]
    private Dropdown roomNameField;

    [SerializeField]
    private Text playerNameField;

    [SerializeField]
    private GameObject connectPanel, readyUI;

    private StrixNetwork strixNetwork;

    // Start is called before the first frame update
    void Start()
    {
        strixNetwork = StrixNetwork.instance;

        webGLHostUrl = "wss://" + appData.webGLHostUrl + ":9122";

        strixNetwork.applicationId = appData.applicationID;

        strixNetwork.ConnectMasterServer(webGLHostUrl,
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
        else if (roomNameField.value == 3)
        {
            roomName = "Room_D";
        }
        else if (roomNameField.value == 4)
        {
            roomName = "Room_E";
        }
        else if (roomNameField.value == 5)
        {
            roomName = "Room_F";
        }
        else if (roomNameField.value == 6)
        {
            roomName = "Room_G";
        }
        else if (roomNameField.value == 7)
        {
            roomName = "Room_H";
        }
        else if (roomNameField.value == 8)
        {
            roomName = "Room_I";
        }
        else if (roomNameField.value == 9)
        {
            roomName = "Room_J";
        }
        else if (roomNameField.value == 10)
        {
            roomName = "Room_K";
        }
        else if (roomNameField.value == 11)
        {
            roomName = "Room_L";
        }
        else if (roomNameField.value == 12)
        {
            roomName = "Room_M";
        }
        Debug.Log(roomName);
    }

    public void SerchRoom()
    {
        AudioController.Instance.PlaySe(0);
        playerName = playerNameField.text.ToString();

        strixNetwork.SearchRoom
            (
                condition: ConditionBuilder.Builder().Field("name").EqualTo(roomName).Build(),
                limit: 1,
                offset: 0,
                handler: searchResults => 
                {
                    if(searchResults.roomInfoCollection.Count < 1)
                    {
                        CreatRoom();
                    }
                    else
                    {
                        Debug.Log(searchResults.roomInfoCollection.Count + " rooms found.");

                        foreach (var roomInfo in searchResults.roomInfoCollection)
                            JoinRoom(roomInfo.host, roomInfo.port, roomInfo.protocol, roomInfo.id);
                    }

                },
                failureHandler: searchError => Debug.Log("Search failed. Reason: " + searchError.cause)
            );
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
        connectPanel.SetActive(false);
        readyUI.SetActive(true);
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
                        { "state", 0 }  // ?????????? "Not Ready"
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
}
