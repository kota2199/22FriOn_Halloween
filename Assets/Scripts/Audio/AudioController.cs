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
        source = GetComponent<AudioSource>();
    }

    public AudioController PlaySe(int index)
    {
        for (int i = 0; i < audioData.Se.Length; i++)
        {
            if (i == index)
            {
                source.clip = audioData.Se[i].Source;
                source.Play();
                StartCoroutine(Checking(source, () =>
                {
                    //音が鳴りやんだら非アクティブにする
                    source.gameObject.SetActive(false);
                }));
            }
        }

        return this;


    }

    public AudioController PlayJingle(int index)
    {
        for (int i = 0; i < audioData.Jingles.Length; i++)
        {
            if (i == index)
            {
                source.clip = audioData.Jingles[i].Source;
                source.Play();
                StartCoroutine(Checking(source, () =>
                {
                    source.gameObject.SetActive(false);
                }));
            }
        }

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
        for (int i = 0; i < audioData.Bgm.Length; i++)
        {
            if (i == index)
            {
                if (source.isPlaying)
                {
                    source.Stop();
                }
                source.clip = audioData.Bgm[i].Source;
                source.Play();
            }
        }
        return this;
    }

    public AudioController StopBgm()
    {
        if (source.isPlaying)
            source.Stop();
        return this;
    }
}