using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SoftGear.Strix.Unity.Runtime;

public class PlayerStatus : StrixBehaviour
{
    [StrixSyncField]
    public int hp = 5;

    [SerializeField]
    Slider hpBar;
    // Start is called before the first frame update
    void Start()
    {
        hpBar.maxValue = hp;
        hpBar.value = hpBar.maxValue;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isLocal)
        {
            return;
        }
        if(other.gameObject.tag == "Bullet")
        {
            RpcToAll("OnHit");
        }
    }
    [StrixRpc]
    public void OnHit()
    {
        hp -= 1;
        hpBar.value = hp;
    }
}
