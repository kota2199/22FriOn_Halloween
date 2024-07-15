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

    [SerializeField]
    private AudioSource seSource;

    [SerializeField]
    private AudioSource bgmSource;

    private void Awake()
    {
        if (instance == null)
        {
            seSource = GetComponent<AudioSource>();
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public AudioController PlaySe(int index)
    {
        GameObject audioObj = new GameObject();
        audioObj.AddComponent<AudioSource>();
        seSource = audioObj.GetComponent<AudioSource>();
        seSource.clip = audioData.Se[index].Source;
        seSource.Play();
        StartCoroutine(Checking(seSource, () =>
        {
            Destroy(audioObj);
        }));

        return this;
    }

    public AudioController ChangeSceneWithSE(int index, int sceneIndex)
    {
        GameObject audioObj = new GameObject();
        audioObj.AddComponent<AudioSource>();
        seSource = audioObj.GetComponent<AudioSource>();
        seSource.clip = audioData.Se[index].Source;
        seSource.Play();

        StartCoroutine(Checking(seSource, () =>
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
        seSource = audioObj.GetComponent<AudioSource>();
        seSource.clip = audioData.Jingles[index].Source;
        seSource.Play();
        StartCoroutine(Checking(seSource, () =>
        {
            seSource.gameObject.SetActive(false);
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
        if (bgmSource && bgmSource.isPlaying)
        {
            bgmSource.Stop();
        }
        GameObject audioObj = new GameObject();
        audioObj.AddComponent<AudioSource>();
        bgmSource = audioObj.GetComponent<AudioSource>();
        bgmSource.clip = audioData.Bgm[index].Source;
        bgmSource.Play();
        return this;
    }

    public AudioController StopBgm()
    {
        if (bgmSource.isPlaying)
            bgmSource.Stop();
        return this;
    }
}