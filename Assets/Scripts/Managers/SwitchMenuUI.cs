using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchMenuUI : MonoBehaviour
{
    public GameObject title, setPlayerName, serchRoom, createRoom, readyCanvas, gameCanvas;

    public GameObject prepareCanvas;

    public enum Status
    {
        Title,
        PlayerName,
        SearchName,
        CreateRoom,
    }

    public Status status;

    public void ChangeUI(GameObject disableObj)
    {
        disableObj.SetActive(false);

        switch (status)
        {
            case Status.Title:

                break;
        }
    }

    public void ToSetPlayerName(GameObject disableObj)
    {
        disableObj.SetActive(false);
        setPlayerName.SetActive(true);
    }

    public void ToCreateRoom(GameObject disableObj)
    {
        disableObj.SetActive(false);
        createRoom.SetActive(true);
    }

    public void ToSerchRoom(GameObject disableObj)
    {
        disableObj.SetActive(false);
        serchRoom.SetActive(true);
    }

    public void ToReady(GameObject disableObj)
    {
        disableObj.SetActive(false);
        prepareCanvas.SetActive(false);
        readyCanvas.SetActive(true);
    }

    public void ToGame(GameObject disableObj)
    {
        disableObj.SetActive(false);
        gameCanvas.SetActive(true);
    }

    public void ToTitle(GameObject disableObj)
    {
        disableObj.SetActive(false);
        title.SetActive(true);
    }
}
