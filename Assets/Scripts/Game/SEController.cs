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
        //�V���O���g�����C���X�^���X��
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
    /// SE��炷
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
                    //�������񂾂��A�N�e�B�u�ɂ���
                    source.gameObject.SetActive(false);
                }));
            }
        }

        return this;


    }

    /// <summary>
    /// SE��炷
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
                    //�������񂾂��A�N�e�B�u�ɂ���
                    source.gameObject.SetActive(false);
                }));
            }
        }

        return this;


    }

    /// <summary>
    /// ����炵�I��������ǂ����Ď����鏈��
    /// </summary>
    /// <param name="source"></param>
    /// <param name="action">������I������ۂ̏����������ɏ���</param>
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
    /// BGM��炷
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