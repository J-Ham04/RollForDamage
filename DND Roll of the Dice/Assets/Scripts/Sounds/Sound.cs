using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0, 1)]
    public float volume;

    //Pitch Variables
    [Range(-3, 3)]
    public float minPitch;
    [Range(-3, 3)]
    public float maxPitch;
    public float pitch
    {
        get
        {
            return Random.Range(minPitch, maxPitch);
        }
    }

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}
