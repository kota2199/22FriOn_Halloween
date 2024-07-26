using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Client.Match.Room.Model;
using SoftGear.Strix.Unity.Runtime;

public class JoinRoomBtnController : StrixBehaviour
{
    public string roomName;

    public ConnectToRoom roomConnector;

    public RoomInfo roomInfo;

    public void JoinToRoom()
    {
        roomConnector.JoinRoom(roomInfo.host, roomInfo.port, roomInfo.protocol, roomInfo.id);
    }
}
