using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance => instance;
    private static SceneController instance;
    
    [SerializeField]
    private SceneData _sceneData;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToNextScene(int index)
    {
        SceneManager.LoadScene(_sceneData.SceneName[index]);
    }

    public void ToNextSceneWithSe(int sceneIndex)
    {
        AudioController.Instance.ChangeSceneWithSE(0,sceneIndex);
    }
}
