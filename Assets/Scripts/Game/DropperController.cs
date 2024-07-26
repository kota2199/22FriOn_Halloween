using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Unity.Runtime;

public class DropperController : StrixBehaviour
{
    private bool isRight = false, isLeft = false;

    [SerializeField]
    private KeyCode leftKey = KeyCode.A, rightKey = KeyCode.D, dropKey = KeyCode.Return;


    private DropController dropController;

    private void Awake()
    {
        dropController = GetComponent<DropController>();
    }
    // Update is called once per frame
    void Update()
    {
        if (!isLocal)
        {
            return;
        }

        //isRight, isLeftがtrueのときにプレイヤーを左右に移動させる。
        float currentPosX = transform.position.x;
        if (isRight && currentPosX < 3.5f)
        {
            transform.position += new Vector3(2f, 0, 0) * Time.deltaTime;
        }
        if (isLeft && -3.5f < currentPosX)
        {
            transform.position -= new Vector3(2f, 0, 0) * Time.deltaTime;
        }

        //PC操作の入力を受け取る
        if(Input.GetKeyDown(rightKey))
        {
            PushRightBtn();
        }
        if (Input.GetKeyUp(rightKey))
        {
            ReleaseRightBtn();
        }

        if (Input.GetKeyDown(leftKey))
        {
            PushLeftBtn();
        }
        if (Input.GetKeyUp(leftKey))
        {
            ReleaseLeftBtn();
        }

        if (Input.GetKeyDown(dropKey))
        {
            DropBtn();
        }
    }

    /// <summary>
    /// 画面上のボタンを押した、離したときに呼び出される
    /// </summary>
    public void PushRightBtn()
    {
        if (!isLocal)
        {
            return;
        }
        isRight = true;
    }
    public void ReleaseRightBtn()
    {
        if (!isLocal)
        {
            return;
        }
        isRight = false;
    }
    public void PushLeftBtn()
    {
        if (!isLocal)
        {
            return;
        }
        isLeft = true;
    }
    public void ReleaseLeftBtn()
    {
        if (!isLocal)
        {
            return;
        }
        isLeft = false;
    }

    public void DropBtn()
    {
        if (!isLocal)
        {
            return;
        }
        dropController.DropPiece();
    }
}
