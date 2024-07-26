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
    private GameObject readyUi, countDownTxt, waitingUi;

    [SerializeField]
    private Text countDownText;

    [SerializeField]
    private StrixNetwork strixNetwork;

    [SerializeField]
    private Button readyButton;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject backPanel;

    //Owner's Game Start Button.
    [SerializeField]
    private GameObject startButton;

    [SerializeField]
    private SwitchMenuUI uiSwitcher;
    
    // Start is called before the first frame update
    void Start()
    {
        if (strixNetwork == null)
        {
            strixNetwork = StrixNetwork.instance;
        }

    }

    // Update is called once per frame
    void Update()
    {
        strixNetwork.roomSession.roomClient.RoomSetMemberNotified += roomSetArgs =>
        {
            RpcToOtherMembers(nameof(HideStartButton));
            RpcToRoomOwner(nameof(DisplayStartButton));
        };
    }

    [StrixRpc]
    void DisplayStartButton()
    {
        startButton.SetActive(true);
    }

    [StrixRpc]
    void HideStartButton()
    {
        startButton.SetActive(false);
    }

    //Called by button
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
                       readyButton.gameObject.SetActive(false);
                       countDownTxt.SetActive(true);
                   },
                   args => {
                       Debug.Log("SetRoomMember failed. error = " + args.cause);
                   }
               );

    }

    //Called by owner's start button
    public void OwnersStart()
    {
        if (CheckAllRoomMembersState())
        {
            RpcToAll(nameof(CallCountDown));
            startButton.SetActive(false);
        }
    }

    [StrixRpc]
    private void CallCountDown()
    {
        StartCoroutine(CountDown());
    }

    [StrixRpc]
    private IEnumerator CountDown()
    {
        readyUi.SetActive(false);
        countDownTxt.SetActive(true);

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
        GameObject.FindWithTag("Manager").GetComponent<TimeManager>().CountStart();

        uiSwitcher.ToGame(uiSwitcher.readyCanvas);
    }

    [StrixRpc]
    public static bool CheckAllRoomMembersState()
    {
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
