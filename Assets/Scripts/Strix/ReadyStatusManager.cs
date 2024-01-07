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
    private GameObject readyUi, countDownUi, waitingUi;

    [SerializeField]
    private Text countDownText;

    private StrixNetwork strixNetwork;

    [SerializeField]
    private Button readyButton;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject backPanel;
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
        AudioController.Instance.PlaySe(0);
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
                       checkAllMembers();
                   },
                   args => {
                       Debug.Log("SetRoomMember failed. error = " + args.cause);
                   }
               );
        readyUi.SetActive(false);
        countDownUi.SetActive(true);
    }
    public void checkAllMembers()
    {
        if (CheckAllRoomMembersState() && strixNetwork.room.GetMemberCount() > 1)
        {
            if (!isLocal)
            {
                return;
            }
            CallCountDown();
        }
    }

    public void CallCountDown()
    {
        StartCoroutine(CountDown());
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

        Color currentColor = backPanel.GetComponent<Image>().color;
        currentColor.a = 0f;
        backPanel.GetComponent<Image>().color = currentColor;

        DropController dropController = player.GetComponent<DropController>();
        dropController.RoomJoined();
        //RpcToAll(nameof(dropController.GeneratePiece));
        GameObject.FindWithTag("Manager").GetComponent<TimeManager>().CountStart();
        waitingUi.SetActive(false);
    }
    public static bool CheckAllRoomMembersState()
    {
        var room = StrixNetwork.instance.room;
        foreach (var roomMember in StrixNetwork.instance.roomMembers)
        {
            if (!roomMember.Value.GetProperties().TryGetValue("state",
                out object value))
            {
                return false;
            }

            if ((int)value != 1)
            {
                return false;
            }
        }

        Debug.Log("CheckAllRoomMembersState OK");
        return true;
    }
}
