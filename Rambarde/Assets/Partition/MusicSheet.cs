using System;
using UnityEngine;

namespace Partition {
    public class MusicSheet : MonoBehaviour {

        public bool autoUpdate = true;
        public int nbrMeasure = 4;
        public int nbrBeat = 4;
        [Range(0, 5)]
        public float beatSize = 2.0f;
        public float height = 3.0f;
        public float tempo = 128;
        
        public GameObject sheetObj;
        public GameObject staveObj; // empty objects with lines as children
        public GameObject rythmeterObj;

        public MelodyData melody;

        private Rythmeter _rythmeter;
        
        private void Start() {
            
            rythmeterObj.AddComponent<Rythmeter>();
            _rythmeter = rythmeterObj.GetComponent<Rythmeter>();
            _rythmeter.OnRyhthmEnd((() => { Debug.Log(melody);}));
            Generate();

            _rythmeter.StartRythm();
            melody = new MelodyData();
        }

        public void Generate() {
            float xSize = beatSize * nbrBeat * nbrMeasure;

            Transform sheetTransform = sheetObj.transform;
            sheetTransform.localScale = new Vector3(xSize, this.height, 1);
            float beatInterval = xSize / (nbrBeat * nbrMeasure + 1);
            float x = (nbrBeat * nbrMeasure / 2.0f -0.5f) * -beatInterval;

            int iChild = 0;
            foreach (Transform child in sheetTransform) {
                if (iChild > nbrBeat * nbrMeasure) {
                    DestroyImmediate(child.gameObject);
                    continue;
                }
                
                child.position = new Vector3(x, 0, 0);
                x += beatInterval;
                child.localScale = new Vector3(beatSize / 8 / sheetTransform.localScale.x, 1.05f, 1.07f);
                ++iChild;
            }

            for (int i = iChild; i <= nbrBeat * nbrMeasure; ++i) {
                GameObject childObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Transform child = childObject.transform;
                child.parent = sheetTransform;
                child.position = new Vector3(x, 0, 0);
                x += beatInterval;
                childObject.transform.localScale = new Vector3(beatSize / 8 / sheetTransform.localScale.x, 1.01f, 1.07f);
            }
            
            
            Transform staveTransform = staveObj.transform;
            staveTransform.localScale = new Vector3(xSize, height, 1);
            float heightInterval = 0.25f; // 4 == nbr of lines on stave
            float h = -1.5f * heightInterval;
            foreach (Transform child in staveObj.transform) {
                child.localPosition = new Vector3(0, h, 0);
                h += heightInterval;
                child.localScale = new Vector3(1, 0.03f, 1.1f);
            }
            
            rythmeterObj.transform.localScale = new Vector3(beatSize / 3, 1.1f * height, 1.2f);
            rythmeterObj.transform.position = new Vector3(-xSize / 2, 0, 0);
            _rythmeter.distance = xSize;
            _rythmeter.duration = 60 * (nbrBeat * nbrMeasure + 1) / tempo;
        }

        private void Update() {
            int inputNote = MusicInput.GetInput();

            int beat = Mathf.RoundToInt(4 * tempo * (Time.time - _rythmeter.startTime) / 60.0f);
            if (melody.Length < beat-1) {
                melody.PushNote('-');
            }
            if (melody.Length < beat) {
                if (inputNote > 0) {
                    melody.PushNote(inputNote.ToString()[0]);
                }
            }
            
        }
    }
}
