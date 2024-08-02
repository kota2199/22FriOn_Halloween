using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StrixAppData", menuName = "CreateData/StrixAppData")]
public class StrixAppData : ScriptableObject
{
    public string applicationID, hostUrl;
}
