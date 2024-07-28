using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMenuUI : MonoBehaviour
{
    public GameObject title, setPlayerName, serchRoom, createRoom, readyCanvas, gameCanvas;

    public GameObject prepareCanvas;

    //プレイヤーネーム表示UIを表示
    public void ToSetPlayerName(GameObject disableObj)
    {
        AudioController.Instance.PlaySe(0);
        disableObj.SetActive(false);
        setPlayerName.SetActive(true);
    }

    //ルーム作成UIを表示
    public void ToCreateRoom(GameObject disableObj)
    {
        AudioController.Instance.PlaySe(0);
        disableObj.SetActive(false);
        createRoom.SetActive(true);
    }

    //ルーム検索UIを表示
    public void ToSerchRoom(GameObject disableObj)
    {
        AudioController.Instance.PlaySe(0);
        disableObj.SetActive(false);
        serchRoom.SetActive(true);
    }

    //ゲーム準備画面を表示
    public void ToReady(GameObject disableObj)
    {
        AudioController.Instance.PlaySe(0);
        disableObj.SetActive(false);
        prepareCanvas.SetActive(false);
        readyCanvas.SetActive(true);
    }

    //ゲーム画面を表示
    public void ToGame(GameObject disableObj)
    {
        AudioController.Instance.PlaySe(0);
        disableObj.SetActive(false);
        gameCanvas.SetActive(true);
    }

    //タイトル画面を表示
    public void ToTitle(GameObject disableObj)
    {
        AudioController.Instance.PlaySe(0);
        disableObj.SetActive(false);
        title.SetActive(true);
    }
}
