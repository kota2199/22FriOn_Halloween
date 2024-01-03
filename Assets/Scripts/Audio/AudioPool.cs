using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPool : MonoBehaviour
{
    [SerializeField]
    private GameObject audioPrefab;

    [SerializeField]
    private int poolSize = 20;

    List<AudioSource> audioPool;

    private static AudioPool Instance;

    private void Awake()
    {
        //�C���X�^���X��
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);

        audioPool = new();

        //�ŏ��ɐ�������
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(audioPrefab, transform);
            obj.SetActive(false);
            audioPool.Add(obj.GetComponent<AudioSource>());
        }
    }

    public AudioSource UseAudioSource()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!audioPool[i].isActiveAndEnabled)
            {
                audioPool[i].gameObject.SetActive(true);
                return audioPool[i];
            }
        }

        //�Ȃ��ꍇ�͐V�����쐬����
        poolSize++;
        GameObject obj = Instantiate(audioPrefab, transform);
        AudioSource sourse = obj.GetComponent<AudioSource>();
        audioPool.Add(sourse);
        return sourse;
    }
}