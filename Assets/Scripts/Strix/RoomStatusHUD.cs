using System.Collections.Generic;
using SoftGear.Strix.Client.Match.Room.Model;
using UnityEngine;
using UnityEngine.UI;

namespace SoftGear.Strix.Unity.Runtime
{
    public class RoomStatusHUD : MonoBehaviour
    {
        public Text RoomNameText;
        public Text RoomMemberList;

        void Update()
        {
            var strixNetwork = StrixNetwork.instance;

            if (strixNetwork == null)
                return;

            CustomizableMatchRoom room = strixNetwork.roomSession.room;

            if (RoomNameText != null)
            {
                RoomNameText.text = (room != null ? room.GetName() : "");
            }

            if (RoomMemberList != null)
            {
                string text = "";

                if (strixNetwork.sortedRoomMembers != null)
                {
                    foreach (var entry in strixNetwork.sortedRoomMembers)
                    {
                        CustomizableMatchRoomMember member = (CustomizableMatchRoomMember)entry;
                        string properties = "";

                        if (member.GetProperties() != null)
                        {
                            foreach (KeyValuePair<string, object> v in member.GetProperties())
                            {
                                properties += " " + v.Key + ":" + v.Value;
                            }
                        }

                        text += member.GetName() + properties + "\n";
                    }
                }

                RoomMemberList.text = text;
            }
        }
    }
}