using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropperController : MonoBehaviour
{
    // Start is called before the first frame update
    private bool isRight = false, isLeft = false;

    // Update is called once per frame
    void Update()
    {
        float currentPosX = transform.position.x;
        if (isRight && currentPosX < 3.5f)
        {
            transform.position += new Vector3(2f, 0, 0) * Time.deltaTime;
        }
        if (isLeft && -3.5f < currentPosX)
        {
            transform.position -= new Vector3(2f, 0, 0) * Time.deltaTime;
        }
    }

    public void PushRightBtn()
    {
        isRight = true;
    }
    public void ReleaseRightBtn()
    {
        isRight = false;
    }
    public void PushLeftBtn()
    {
        isLeft = true;
    }
    public void ReleaseLeftBtn()
    {
        isLeft = false;
    }

    public void DropBtn()
    {
        this.gameObject.GetComponent<DropController>().DropPiece();
    }
}
