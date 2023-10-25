using System.Collections.Generic;
using SoftGear.Strix.Unity.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class StrixEnterRoom : StrixBehaviour {
    /// <summary>
    /// ルームに参加可能な最大人数
    /// </summary>
    public int capacity = 4;

    /// <summary>
    /// ルーム名
    /// </summary>
    /// 
    string roomName = "Room_A";

    public Dropdown roomNameField;

    /// <summary>
    /// ルーム入室完了時イベント
    /// </summary>
    public UnityEvent onRoomEntered;

    /// <summary>
    /// ルーム入室失敗時イベント
    /// </summary>
    public UnityEvent onRoomEnterFailed;

    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocal)
        {
            return;
        }
        Debug.Log(roomName);
    }

    public void EnterRoom() {
        StrixNetwork.instance.JoinRandomRoom(StrixNetwork.instance.playerName, args => {
            onRoomEntered.Invoke();
        }, args => {
            CreateRoom();
        });
    }

    private void CreateRoom() {
        RoomProperties roomProperties = new RoomProperties {
            capacity = capacity,
            name = roomName
    };

        RoomMemberProperties memberProperties = new RoomMemberProperties {
            name = StrixNetwork.instance.playerName
        };


        StrixNetwork.instance.CreateRoom(roomProperties, memberProperties, args => {
            onRoomEntered.Invoke();
        }, args => {
            onRoomEnterFailed.Invoke();
        });
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
}  