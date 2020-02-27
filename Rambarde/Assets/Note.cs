using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    public int note;
    
    private bool _played;

    public bool Played => _played;

    public void Play()
    {
        Debug.Log("note" + note +" played");
        _played = true;
    }
}
