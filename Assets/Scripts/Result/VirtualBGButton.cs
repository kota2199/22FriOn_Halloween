using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VirtualBGButton : MonoBehaviour
{
    [SerializeField] Text buttonText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseEnter()
    {
        buttonText.text = "クリックでDLサイトを開く！";
    }

    public void OnMouseExit()
    {
        buttonText.text = "バーチャル背景はこちら！";
    }
}
