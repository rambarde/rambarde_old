using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class KeyInputManager : MonoBehaviour
{
    public List<SphereCollider> colliders;
    public List<Image> InputImages;

    private Note _currentNote;
    void Start()
    {
        for (int i = 0; i < colliders.Count; i++)
        {
            colliders[i]
                .OnTriggerEnterAsObservable()
                .Where(c => c.gameObject.CompareTag("Note"))
                .Subscribe(c => _currentNote = c.gameObject.GetComponent<Note>())
                .AddTo(this);
            colliders[i]
                .OnTriggerExitAsObservable()
                .Where(c => c.gameObject.CompareTag("Note"))
                .Subscribe(c =>
                {
                    Debug.LogError("missed");
                    TweenSequenceWithDelay(
                        InputImages[_currentNote.note - 1].DOColor(Color.red, 0.2f),
                        InputImages[_currentNote.note - 1].DOColor(Color.white, 0.2f),
                        0.5f);
                    _currentNote = null;
                }).AddTo(this);
        }

        this.UpdateAsObservable()
            .Where(_ => GetInput() != 0)
            .Select(x => GetInput())
            .Subscribe(x =>
            {
                if (_currentNote != null && _currentNote.note == x)
                {
                    _currentNote.Play();
                    Destroy(_currentNote.gameObject);
                    _currentNote = null;
                    
                    TweenSequenceWithDelay(
                        InputImages[x-1].DOColor(Color.green, 0.2f),
                        InputImages[x-1].DOColor(Color.white, 0.2f),
                        0.5f);
                }
                else
                {
                    Debug.LogError("boooooooooh");
                    TweenSequenceWithDelay(
                        InputImages[x-1].DOColor(Color.red, 0.2f),
                        InputImages[x-1].DOColor(Color.white, 0.2f),
                        0.5f);
                }
            }).AddTo(this);
    }

    private void TweenSequenceWithDelay(Tween a, Tween b, float delay)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.Append(a);
        sequence.AppendInterval(delay);
        sequence.Append(b);
        sequence.Play();
    }
    
    public static int GetInput() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            return 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            return 2;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            return 3;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            return 4;
        }

        return 0;
    }
}
