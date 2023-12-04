using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPool : MonoBehaviour
{
    [SerializeField]
    private GameObject _audioPrefab;

    [SerializeField]
    private int _poolSize = 20;

    List<AudioSource> _audioPool;

    private static AudioPool Instance;

    private void Awake()
    {
        //インスタンス化
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);

        _audioPool = new();

        //最初に生成する
        for (int i = 0; i < _poolSize; i++)
        {
            GameObject obj = Instantiate(_audioPrefab, transform);
            obj.SetActive(false);
            _audioPool.Add(obj.GetComponent<AudioSource>());
        }
    }

    public AudioSource UseAudioSource()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            if (!_audioPool[i].isActiveAndEnabled)
            {
                _audioPool[i].gameObject.SetActive(true);
                return _audioPool[i];
            }
        }

        //ない場合は新しく作成する
        _poolSize++;
        GameObject obj = Instantiate(_audioPrefab, transform);
        AudioSource sourse = obj.GetComponent<AudioSource>();
        _audioPool.Add(sourse);
        return sourse;
    }
}