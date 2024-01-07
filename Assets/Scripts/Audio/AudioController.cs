using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField]
    private AudioData audioData;

    public static AudioController Instance => instance;

    private static AudioController instance;

    private AudioSource source;

    private void Awake()
    {
        if (instance == null)
        {
            source = GetComponent<AudioSource>();
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //source = GetComponent<AudioSource>();
    }

    public AudioController PlaySe(int index)
    {
        GameObject audioObj = new GameObject();
        audioObj.AddComponent<AudioSource>();
        source = audioObj.GetComponent<AudioSource>();
        source.clip = audioData.Se[index].Source;
        source.Play();
        StartCoroutine(Checking(source, () =>
        {
            Destroy(audioObj);
        }));

        return this;
    }

    public AudioController ChangeSceneWithSE(int index, int sceneIndex)
    {
        GameObject audioObj = new GameObject();
        audioObj.AddComponent<AudioSource>();
        source = audioObj.GetComponent<AudioSource>();
        source.clip = audioData.Se[index].Source;
        source.Play();

        StartCoroutine(Checking(source, () =>
        {
            Destroy(audioObj);
            SceneController.Instance.ToNextScene(sceneIndex);
        }));

        return this;
    }

    public AudioController PlayJingle(int index)
    {
        GameObject audioObj = new GameObject();
        audioObj.AddComponent<AudioSource>();
        source = audioObj.GetComponent<AudioSource>();
        source.clip = audioData.Jingles[index].Source;
        source.Play();
        StartCoroutine(Checking(source, () =>
        {
            source.gameObject.SetActive(false);
        }));

        return this;


    }

    private IEnumerator Checking(AudioSource source, Action action)
    {
        yield return new WaitWhile(() => source.isPlaying);

        if (action != null)
        {
            action.Invoke();
        }
    }

    public AudioController PlayBgm(int index)
    {
        if (source && source.isPlaying)
        {
            source.Stop();
        }
        GameObject audioObj = new GameObject();
        audioObj.AddComponent<AudioSource>();
        source = audioObj.GetComponent<AudioSource>();
        source.clip = audioData.Bgm[index].Source;
        source.Play();
        return this;
    }

    public AudioController StopBgm()
    {
        if (source.isPlaying)
            source.Stop();
        return this;
    }
}