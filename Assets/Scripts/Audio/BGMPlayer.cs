using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMPlayer : MonoBehaviour
{
    [SerializeField]
    private int selectIndex;

    // Start is called before the first frame update
    void Start()
    {
        AudioController.Instance.PlayBgm(selectIndex);
    }
}