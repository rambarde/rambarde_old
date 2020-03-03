using System.Collections;
using UnityEngine;

namespace Music {
    public class MusicSheet : MonoBehaviour {
        public bool autoUpdate = true;
        
        public int nbrMeasure = 4;
        public int nbrBeat = 4;
        [SerializeField] [Range(0, 5)] private float beatSize = 2.0f;
        [SerializeField] private float height = 3.0f;
        public float tempo = 128;

        [SerializeField] private GameObject sheetObj;
        [SerializeField] private GameObject staveObj; // empty objects with lines as children
        [SerializeField] private GameObject rythmeterObj;
/*
        private int _inputNote;
        private float _startTime;
        private Rythmeter _rythmeter;
        private GameObject _notesHolder;
        private MelodyData _melody;
        private MelodyData _plannedMelody;
        private const int NbrNoteInBeat = 2; // croche :(

        public void StartPlaying(MelodyData melodyData) {
            _plannedMelody = melodyData;
            gameObject.SetActive(true);
            
            _melody = new MelodyData();
            rythmeterObj.AddComponent<Rythmeter>();
            _rythmeter = rythmeterObj.GetComponent<Rythmeter>();
            Generate();

            _rythmeter.StartRythm();
            StartCoroutine(nameof(HandleInput));
            _startTime = Time.time;
            
            Destroy(_notesHolder);
            _notesHolder = new GameObject();
        }

        public void PlaceNote(int beat, int note) {
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Transform cubeTransform = cube.transform;
            Transform sheetTransform = sheetObj.transform;
            Vector3 sheetScale = sheetTransform.localScale;
            cubeTransform.position = sheetTransform.position - sheetScale / 2 + new Vector3(0, sheetScale.y / 8.0f, 0) +
                                     new Vector3(beat * beatSize / NbrNoteInBeat, sheetScale.y / 4.0f * (note - 1), 0);
            cubeTransform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
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

                child.localPosition = new Vector3(x, 0, 0) + sheetTransform.position;
                child.parent = sheetTransform;
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
            rythmeterObj.transform.localPosition = new Vector3(-xSize / 2 - (beatSize * nbrBeat), 0, 0);
            _rythmeter.distance = beatSize * nbrBeat * nbrMeasure;
            _rythmeter.preDistance = beatSize * nbrBeat;
            _rythmeter.duration = 60 * nbrBeat * nbrMeasure / tempo;
            
            _rythmeter.OnRyhthmEnd(() => {
                // Debug.Log(_melody);
                // Debug.Log(_melody.Length);
                //gameObject.SetActive(false);
                Destroy(_notesHolder);
            });
        }

        private void Update() {
            int input = MusicInput.GetInput();
            if (input > 0) {
                _inputNote = input;
            }
        }

        private IEnumerator HandleInput() {

            for (int i = 0; i <= NbrNoteInBeat * nbrBeat * (nbrMeasure + 1); ++i) {
                if (i % NbrNoteInBeat == 0) {
                    GetComponent<AudioSource>().Play();
                }

                if (i > NbrNoteInBeat * nbrBeat) {
                    _melody.PushNote(_inputNote == 0 ? '-' : _inputNote.ToString()[0]);
                    if (_inputNote > 0) {
                        GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        var localScale = staveObj.transform.localScale;
                        cube.transform.position = rythmeterObj.transform.position + new Vector3(0,
                                                      (_inputNote - 1) * (height / 4.0f) + height / 10.0f -
                                                      localScale.y / 2.0f, -localScale.z / 2.0f);
                        cube.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                        cube.transform.parent = _notesHolder.transform;
                    }

                    _inputNote = 0;
                }

                int i1 = i;
                yield return new WaitUntil(
                    () => Time.time - _startTime >= (i1 + 1) * (60.0f / (NbrNoteInBeat * tempo)) - 0.5f * 60.0f / (NbrNoteInBeat * tempo));
            }
        }*/
    }
}