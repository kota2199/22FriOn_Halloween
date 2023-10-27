using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SEController : MonoBehaviour
{
    [SerializeField]
    private SEData _AudioData;

    public static SEController Instance => _instance;
    private static SEController _instance;

    [SerializeField]
    private AudioPool _audioPool;

    private AudioSource _source;

    private void Awake()
    {
        //シングルトンかつインスタンス化
        if (_instance == null)
        {
            _source = GetComponent<AudioSource>();
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    /// <summary>
    /// SEを鳴らす
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public SEController PlaySe(int index)
    {
        for (int i = 0; i < _AudioData.Se.Length; i++)
        {
            if (i == index)
            {
                AudioSource source = _audioPool.UseAudioSource();
                source.clip = _AudioData.Se[i].Source;
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

    /// <summary>
    /// SEを鳴らす
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public SEController PlayJingle(int index)
    {
        for (int i = 0; i < _AudioData.Jingles.Length; i++)
        {
            if (i == index)
            {
                AudioSource source = _audioPool.UseAudioSource();
                source.clip = _AudioData.Jingles[i].Source;
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

    /// <summary>
    /// 音を鳴らし終わったかどうか監視する処理
    /// </summary>
    /// <param name="source"></param>
    /// <param name="action">音が鳴り終わった際の処理をここに書く</param>
    /// <returns></returns>
    private IEnumerator Checking(AudioSource source, Action action)
    {
        yield return new WaitWhile(() => source.isPlaying);

        if (action != null)
        {
            action.Invoke();
        }
    }


    /// <summary>
    /// BGMを鳴らす
    /// </summary>
    /// <param name="index"></param>
    /// <returns></returns>
    public SEController PlayBgm(int index)
    {
        for (int i = 0; i < _AudioData.Bgm.Length; i++)
        {
            if (i == index)
            {
                if (_source.isPlaying)
                {
                    _source.Stop();
                }
                _source.clip = _AudioData.Bgm[i].Source;
                _source.Play();
            }
        }
        return this;
    }

    public SEController StopBgm()
    {
        if (_source.isPlaying)
            _source.Stop();
        return this;
    }
}