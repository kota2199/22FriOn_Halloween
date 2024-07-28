using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneData", menuName = "CreateData/SceneData")]
public class SceneData : ScriptableObject
{
    [SerializeField]
    private Object[] Scene;

    [SerializeField]
    private string[] _SceneName;

    public string[] SceneName => _SceneName;

    public void Save()
    {
        _SceneName = new string[Scene.Length];
        for (int i = 0; i < Scene.Length; i++)
        {
            _SceneName[i] = Scene[i].name;
        }
    }

}