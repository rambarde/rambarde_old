using System;
using System.Collections;
using UnityEditor;
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

        private const int NbrNoteInBeat = 2; // croche :(

        private float _startTime;
        
        private void Start() {
            melody = new MelodyData();
            
            rythmeterObj.AddComponent<Rythmeter>();
            _rythmeter = rythmeterObj.GetComponent<Rythmeter>();
            _rythmeter.OnRyhthmEnd(() => {
                Debug.Log(melody);
                Debug.Log(melody.Length);
                //StopCoroutine(nameof(HandleInput));
            });
            Generate();
            _rythmeter.distance = beatSize * nbrBeat * nbrMeasure;
            _rythmeter.duration = 60 * nbrBeat * nbrMeasure / tempo;
            
            _rythmeter.StartRythm();
            StartCoroutine(nameof(HandleInput));
            _startTime = Time.time;

        }

        public void Generate() {
            float xSize = beatSize * nbrBeat * nbrMeasure;

            Transform sheetTransform = sheetObj.transform;
            sheetTransform.localScale = new Vector3(xSize, this.height, 1);
            float beatInterval = xSize / (nbrBeat * nbrMeasure);
            float x = (nbrBeat * nbrMeasure / 2.0f) * -beatInterval;

            int iChild = 0;
            foreach (Transform child in sheetTransform) {
                DestroyImmediate(child.gameObject);
            }

            for (int i = iChild; i < nbrBeat * nbrMeasure; ++i) {
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
            
            rythmeterObj.transform.localScale = new Vector3(beatSize / 8, 1.1f * height, 1.2f);
            rythmeterObj.transform.position = new Vector3(-xSize / 2, 0, 0);
        }

        private void Update() {
            int input = MusicInput.GetInput();
            if (input > 0) {
                _inputNote = input;
            }
        }

        private int _inputNote = 0;
        private IEnumerator HandleInput() {
            for (int i = 0; i < NbrNoteInBeat * nbrBeat * nbrMeasure; ++i) {
                if (i % NbrNoteInBeat == 0) {
                    GetComponent<AudioSource>().Play();
                }
                melody.PushNote(_inputNote.ToString()[0]);
                if (_inputNote > 0) {
                    GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    var localScale = staveObj.transform.localScale;
                    cube.transform.position = rythmeterObj.transform.position + new Vector3(0,
                                                  (_inputNote - 1) * (height / 4.0f) + height / 10.0f -
                                                  localScale.y / 2.0f, -localScale.z / 2.0f);
                    cube.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                }
                
                _inputNote = 0;
                
                // GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                // sphere.transform.position = rythmeterObj.transform.position + new Vector3(0, 1.5f, -0.5f);
                // sphere.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                
                int i1 = i;
                yield return new WaitUntil(() => Time.time - _startTime >= (i1+1) * (60.0f / (NbrNoteInBeat * tempo)) - 0.5f * 60.0f / (NbrNoteInBeat * tempo));
            }
        }
    }
}
