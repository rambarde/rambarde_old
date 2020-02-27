using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class NoteGarbage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.OnTriggerEnterAsObservable()
            .Where(x => x.gameObject.CompareTag($"Note"))
            .Subscribe(x => Destroy(x.gameObject));
    }
}
