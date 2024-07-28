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
    
    void Start()
    {
        if (strixNetwork == null)
        {
            strixNetwork = StrixNetwork.instance;
        }

    }

    void Update()
    {
        //ルームにメンバーが参加したとき、オーナーにだけゲーム開始ボタンを表示させる
        strixNetwork.roomSession.roomClient.RoomSetMemberNotified += roomSetArgs =>
        {
            RpcToOtherMembers(nameof(HideStartButton));
            RpcToRoomOwner(nameof(DisplayStartButton));
        };
    }

    //ゲーム開始ボタンを表示させる
    [StrixRpc]
    void DisplayStartButton()
    {
        startButton.SetActive(true);
    }

    //ゲーム開始ボタンを非表示にする
    [StrixRpc]
    void HideStartButton()
    {
        startButton.SetActive(false);
    }

    //準備OKボタンを押したとき呼び出される
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

    //オーナーに表示されているゲームスタートボタンが押されたときに呼び出される
    public void OwnersStart()
    {
        //全てのメンバーの準備が完了していたら
        if (CheckAllRoomMembersState())
        {
            RpcToAll(nameof(CallCountDown));
            startButton.SetActive(false);
        }
    }

    //CountDownコルーチンを呼び出す。
    [StrixRpc]
    private void CallCountDown()
    {
        StartCoroutine(CountDown());
    }

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

    //全員の準備が完了しているかをチェックする
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
