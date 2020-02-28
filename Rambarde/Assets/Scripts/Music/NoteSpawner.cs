using System;
using TMPro;
using UnityEngine;

namespace Music {
    public class NoteSpawner : MonoBehaviour {
        
        [SerializeField] private Transform m1, m2, m3, m4;
        [SerializeField] private GameObject notePrefab;

        public void SpawnNote(string note) {
            Transform parent;
            switch (note) {
                case "-" :
                case "_":
                case "*":
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
            noteObj.GetComponent<NoteMove>().speed = 2f;
            noteObj.GetComponentInChildren<TextMeshProUGUI>().text = note;
        }
    }
}
