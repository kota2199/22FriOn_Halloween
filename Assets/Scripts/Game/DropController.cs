using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoftGear.Strix.Unity.Runtime;

public class DropController : StrixBehaviour
{
    public static DropController instance;

    bool isMovingRight = false, isMovingLeft = false;

    public GameObject[] Pieces;
    GameObject SetPiece;

    [SerializeField]
    GameObject dropButton;

    bool dropped = true;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    void Start()
    {
        if (!isLocal)
        {
            return;
        }
        GeneratePiece();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocal)
        {
            return;
        }
        float currentPos = transform.position.x;
        if (isMovingRight && currentPos < 3.5f && !dropped)
        {
            transform.position += new Vector3(2f, 0, 0) * Time.deltaTime;
        }
        else if(isMovingLeft && currentPos > -3 && !dropped)
        {
            transform.position -= new Vector3(2f, 0, 0) * Time.deltaTime;
        }

    }

    public void PushRightButton()
    {
        if (!isLocal)
        {
            return;
        }
        isMovingRight = true;
    }

    public void ReleaseRightButton()
    {
        if (!isLocal)
        {
            return;
        }
        isMovingRight = false;
    }
    public void PushLeftButton()
    {
        if (!isLocal)
        {
            return;
        }
        isMovingLeft = true;
    }

    public void ReleaseLeftButton()
    {
        if (!isLocal)
        {
            return;
        }
        isMovingLeft = false;
    }
    void GeneratePiece()
    {
        if (!isLocal)
        {
            return;
        }
        Debug.Log("ok");

        if (dropped)
        {
            SetPiece = Instantiate(PieceSet(), transform.position, Quaternion.identity);
            SetPiece.transform.SetParent(this.gameObject.transform);
            dropped = false;
            dropButton.SetActive(true);
        }
    }
    [StrixRpc]
    private GameObject PieceSet()
    {
        if (!isLocal)
        {
            return null;
        }
        int randomeNum = Random.Range(0, 5);
        GameObject piece = Pieces[randomeNum];
        return piece;
    }

    public void DropPiece()
    {
        if (!isLocal)
        {
            return;
        }
        SetPiece.transform.parent = null;
        SetPiece.GetComponent<Rigidbody2D>().simulated = true;
        dropped = true;
        dropButton.SetActive(false);
        Invoke("GeneratePiece", 3f);
    }
}
