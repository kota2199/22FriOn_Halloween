using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropController : MonoBehaviour
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
        GeneratePiece();
    }

    // Update is called once per frame
    void Update()
    {
        float currentPos = transform.position.x;
        if (isMovingRight && currentPos < 3 && !dropped)
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
        isMovingRight = true;
    }

    public void ReleaseRightButton()
    {
        isMovingRight = false;
    }
    public void PushLeftButton()
    {
        isMovingLeft = true;
    }

    public void ReleaseLeftButton()
    {
        isMovingLeft = false;
    }

    void GeneratePiece()
    {
        if (dropped)
        {
            SetPiece = Instantiate(PieceSet(), transform.position, Quaternion.identity);
            SetPiece.transform.SetParent(this.gameObject.transform);
            dropped = false;
            dropButton.SetActive(true);
        }
    }

    private GameObject PieceSet()
    {
        int randomeNum = Random.Range(0, 5);
        GameObject piece = Pieces[randomeNum];
        return piece;
    }

    public void DropPiece()
    {
        SetPiece.transform.parent = null;
        SetPiece.GetComponent<Rigidbody2D>().simulated = true;
        dropped = true;
        dropButton.SetActive(false);
        Invoke("GeneratePiece", 3f);
    }
}
