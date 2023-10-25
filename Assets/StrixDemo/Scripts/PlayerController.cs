using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Unity.Runtime;

public class PlayerController : StrixBehaviour
{
    [SerializeField]
    float speed;

    [SerializeField]
    GameObject bullet, bulletLaunchPos;
    void Update()
    {
        if (!isLocal)
        {
            return;
        }
        float inputHorizontal = Input.GetAxis("Horizontal");
        float inputVertical = Input.GetAxis("Vertical");

        this.transform.position += new Vector3(inputHorizontal, 0, inputVertical) * Time.deltaTime;

        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(bullet, bulletLaunchPos.transform.position, Quaternion.identity);
        }
    }
}
