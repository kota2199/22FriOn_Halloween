using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoftGear.Strix.Client.Core.Model.Manager.Filter;
using SoftGear.Strix.Client.Core.Model.Manager.Filter.Builder;
using SoftGear.Strix.Client.Match.Room.Model;
using SoftGear.Strix.Unity.Runtime;
using System.Linq;

public class ReadyStatusManager : StrixBehaviour
{
    [SerializeField]
    GameObject readyUi, countDownUi, waitingUi;

    [SerializeField]
    Text countDownText;

    StrixNetwork strixNetwork;

    [SerializeField]
    Button readyButton;
    // Start is called before the first frame update
    void Start()
    {
        strixNetwork = StrixNetwork.instance;
    }

    // Update is called once per frame
    void Update()
    {
        strixNetwork.roomSession.roomClient.RoomSetMemberNotified += roomSetArgs =>
        {
            checkAllMembers();
        };
    }
    public void OnReady()
    {
        strixNetwork.SetRoomMember
               (
                   strixNetwork.selfRoomMember.GetPrimaryKey(),
                   new Dictionary<string, object>()
                   {
                    { "properties", new Dictionary<string, object>()
                        {
                            { "state", 1 }
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
        readyUi.SetActive(false);
        countDownUi.SetActive(true);
        //FOR DEBUG
        //StartCoroutine(CountDown());
    }
    public void checkAllMembers()
    {
        if (CheckAllRoomMembersState(1))
        {
            StartCoroutine(CountDown());
        }
    }
    private IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1);
        countDownText.text = "3";
        yield return new WaitForSeconds(1);
        countDownText.text = "2";
        yield return new WaitForSeconds(1);
        countDownText.text = "1";
        yield return new WaitForSeconds(1);
        countDownText.text = "Go!";
        yield return new WaitForSeconds(1);
        waitingUi.SetActive(false);
    }
    public static bool CheckAllRoomMembersState(int desiredState)
    {
        foreach (var roomMember in StrixNetwork.instance.roomMembers)
        {
            if (!roomMember.Value.GetProperties().TryGetValue("state",
                out object value))
            {
                return false;
            }

            if ((int)value != desiredState)
            {
                return false;
            }
        }

        Debug.Log("CheckAllRoomMembersState OK");
        return true;
    }
}
