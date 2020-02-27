using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class NoteMove : MonoBehaviour
{
    // Start is called before the first frame update
    void FixedUpdate() {
        transform.position += Vector3.right * (Time.fixedDeltaTime * 2f);
    }
}
