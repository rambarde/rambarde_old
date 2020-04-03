using UnityEngine;

public class BuzzSound : MonoBehaviour
{
    public AudioSource myAudioSource;
    public float Play()
    {
        myAudioSource.Play();
        return myAudioSource.clip.length;
    }
}