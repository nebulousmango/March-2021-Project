using UnityEngine;

[System.Serializable]
public class SoundData
{
    [HideInInspector] public AudioSource AudioSource;
    public string S_Name;
    public AudioClip AudioClip;
    [Range(0,1)] public float F_Volume;
    public bool B_PlayOnAwake;
    public bool B_Loop;
}
