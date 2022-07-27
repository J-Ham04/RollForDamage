using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static float musicVolume;
    public static float sfxVolume;

    private void Awake()
    {
        musicVolume = PlayerPrefs.GetFloat("Music Volume");
        sfxVolume = PlayerPrefs.GetFloat("Sfx Volume");

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            switch (s.soundType)
            {
                default:
                    break;
                case Sound.SoundType.Music:
                    s.source.volume = s.volume * musicVolume;
                    break;
                case Sound.SoundType.Sfx:
                    s.source.volume = s.volume * sfxVolume;
                    break;
            }
            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        Play("Main Music");
    }

    private void RefreshVolumeLevels()
    {
        foreach (Sound s in sounds)
        {
            switch (s.soundType)
            {
                default:
                    break;
                case Sound.SoundType.Music:
                    s.source.volume = s.volume * musicVolume;
                    break;
                case Sound.SoundType.Sfx:
                    s.source.volume = s.volume * sfxVolume;
                    break;
            }

        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, Sound => Sound.name == name);
        if(s == null)
        {
            return;
        }
        s.source.pitch = s.pitch;
        s.source.Play();
    }

    public void AdjustMusicVolume(float value)
    {
        musicVolume = value;
        RefreshVolumeLevels();
    }

    public void AdjustSfxVolume(float value)
    {
        sfxVolume = value;
        RefreshVolumeLevels();
    }
}
