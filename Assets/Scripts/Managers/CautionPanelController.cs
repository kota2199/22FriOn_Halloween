using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CautionPanelController : MonoBehaviour
{

    [SerializeField]
    private Text messageText;

    [SerializeField]
    private GameObject cautionPanel;

    public void DisplayCaution(string message)
    {
        messageText.text = message;
        cautionPanel.SetActive(true);
    }

    public void HideCaution()
    {
        cautionPanel.SetActive(false);
    }
}
