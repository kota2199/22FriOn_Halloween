using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollideManager : MonoBehaviour
{

    public enum TypeOfPiece
    {
        Type1, Type2, Type3, Type4, Type5, Type6, Type7, Type8, Type9, Type10, Type11
    }

    public TypeOfPiece typeOfPiece;

    [SerializeField]
    GameObject testObj;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<CollideManager>()
            && collision.gameObject.GetComponent<CollideManager>().typeOfPiece == typeOfPiece)
        {
            CollideAndGenerateManager.instance.CollideNotice
                (this.gameObject, collision.gameObject, typeOfPiece);
            Destroy(this.gameObject);
        }
    }
}
