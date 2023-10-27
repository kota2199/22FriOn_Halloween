using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioData", menuName = "CreateData/AudioData")]
public class SEData : ScriptableObject
{
    public SE[] Se;
    public Jingle[] Jingles;
    public BGM[] Bgm;

    [System.Serializable]
    public class SE
    {
        public AudioClip Source;
    }

    [System.Serializable]
    public class Jingle
    {
        public AudioClip Source;
    }

    [System.Serializable]
    public class BGM
    {
        public AudioClip Source;
    }
}