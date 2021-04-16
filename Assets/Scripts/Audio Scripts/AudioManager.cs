using UnityEngine;

public class AudioManager : MonoBehaviour
{
    //Variable to access SoundData class.
    public SoundData[] soundsData;

    private void Awake()
    {
        //Creates AudioSource components in this object.
        CreateSources();
    }

    private void Start()
    {
        //Plays Music audio.
        PlaySound("Music");
    }

    //Creates AudioSource component for all items in SoundData class.
    void CreateSources()
    {
        foreach(SoundData soundData in soundsData)
        {
            AudioSource src = gameObject.AddComponent<AudioSource>();
            soundData.AudioSource = src;
            soundData.AudioSource.clip = soundData.AudioClip;
            soundData.AudioSource.volume = soundData.F_Volume;
            soundData.AudioSource.playOnAwake = soundData.B_PlayOnAwake;
            soundData.AudioSource.loop = soundData.B_Loop;
        }
    }

    //Function to play AudioSource.
    public void PlaySound(string name)
    {
        foreach(SoundData sd in soundsData)
        {
            if (sd.S_Name == name) sd.AudioSource.Play();
        }
    }

    //Function to stop AudioSource.
    public void StopSound(string name)
    {
        foreach (SoundData sd in soundsData)
        {
            if (sd.S_Name == name) sd.AudioSource.Stop();
        }
    }
}
