using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using Melodies;
using UniRx;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private static MusicManager _instance;
    public static MusicManager Instance => _instance;

    public AudioSource melodySource;
    public AudioClip melodyDefault;
    public BuzzSound buzzSource;

    public void Awake()
    {
        _instance = this;
    }



    public void PlayBuzz()
    {
        melodySource.volume = 0;
        var buzz = GameObject.Instantiate(buzzSource);
        var buzzLen = buzz.Play();
        melodySource.DOFade(1, buzzLen);
        Destroy(buzz.gameObject, buzzLen);
    }

    internal async Task PlayMelodies(ReactiveCollection<Melody> selectedMelodies)
    {
        Debug.Log("play melodies");
        await Utils.AwaitObservable(Observable.Timer(TimeSpan.FromSeconds(4)));
        foreach (var melody in selectedMelodies)
        {
           //Debug.Log(melody.clip.name ?? "default");
            melodySource.clip = melody.clip != null ? melody.clip : melodyDefault ;
            melodySource.Play();
            await Utils.AwaitObservable(Observable.Timer(TimeSpan.FromSeconds(melodySource.clip.length)));
        }

    }
}
