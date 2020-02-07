using System;
using UnityEngine;

namespace Partition {
    public class MusicSheet : MonoBehaviour {

        public bool autoUpdate = true;
        public int nbrMeasure = 4;
        public int nbrBeat = 4;
        [Range(0, 1)]
        public float beatSize = 2.0f;
        public float height = 3.0f;
        public float tempo = 128;
        
        public GameObject sheet;
        public GameObject stave; // empty objects with lines as children
        public GameObject rythmeter;

        private void Start() {
            Generate();
            rythmeter.GetComponent<Rythmeter>().StartRythm();
        }

        public void Generate() {
            float xSize = beatSize * nbrBeat * nbrMeasure;

            Transform sheetTransform = sheet.transform;
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
            
            
            Transform staveTransform = stave.transform;
            staveTransform.localScale = new Vector3(xSize, height, 1);
            float heightInterval = 0.25f; // 4 == nbr of lines on stave
            float h = -1.5f * heightInterval;
            foreach (Transform child in stave.transform) {
                child.localPosition = new Vector3(0, h, 0);
                h += heightInterval;
                child.localScale = new Vector3(1, 0.03f, 1.1f);
            }
            
            rythmeter.transform.localScale = new Vector3(beatSize / 3, 1.1f * height, 1.2f);
            rythmeter.transform.position = new Vector3(-xSize / 2, 0, 0);
            rythmeter.GetComponent<Rythmeter>().distance = xSize;
            rythmeter.GetComponent<Rythmeter>().duration = 60 * (nbrBeat*beatSize * nbrMeasure + 1) / tempo;
        }

        private void Update() {
            int input = MusicInput.GetInput();
            if (input > 0) {
                Debug.Log(input);
            }
        }
    }
}
