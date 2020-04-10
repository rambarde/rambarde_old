using System;
using Melodies;
using TMPro;
using UnityEngine;

namespace Music {
    public class NoteSpawner : MonoBehaviour {
        
        [SerializeField] private Transform m1, m2, m3, m4;
        [SerializeField] private GameObject notePrefab;

        public void SpawnNote(string note, Melody melody) {
            Transform parent;
            switch (note) {
                case "_":
                case "*":
                    return;
                case "-" :
                    melody.score.Value += 1;
                    return;
                
                case "1" :
                    parent = m1;
                    break;
                case "2" :
                    parent = m2;
                    break;
                case "3" :
                    parent = m3;
                    break;
                case "4" :
                    parent = m4;
                    break;
                default:
                    Debug.Log("warning : tried to spawn a note of unkown type : [" + note + "]");
                    return;
            }

            GameObject noteObj = Instantiate(notePrefab, parent);
            noteObj.GetComponent<Note>().note = int.Parse(note);
            noteObj.GetComponent<Note>().melody = melody;
            noteObj.GetComponent<NoteMove>().speed = 200f;
            noteObj.GetComponentInChildren<TextMeshProUGUI>().text = note;
        }
    }
}
