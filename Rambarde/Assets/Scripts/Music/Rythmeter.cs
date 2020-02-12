using System;
using System.Collections;
using UnityEngine;

namespace Partition {
    public class Rythmeter : MonoBehaviour {

        public float preDistance;
        public float distance;
        public float duration;

        public float startTime;

        private Action _rythmStart = () => { };
        private Action _rythmEnd = () => { };

        public void StartRythm() {
            StartCoroutine(nameof(MoveRythm));
            startTime = Time.time;
        }

        private IEnumerator MoveRythm() {
            float speed = distance / duration;
            float walked = 0;

            while (walked < preDistance) {
                Transform tf = transform;
                tf.position += new Vector3(Time.deltaTime * speed, 0, 0);
                walked += Time.deltaTime * speed;
                yield return null;
            }
            
            _rythmStart.Invoke();
            walked = 0;
            
            while (walked < distance) {
                Transform tf = transform;
                tf.position += new Vector3(Time.deltaTime * speed, 0, 0);
                walked += Time.deltaTime * speed;
                yield return null;
            }

            _rythmEnd.Invoke();
        }
        
        public void OnRyhthmStart(Action toCall) {
            _rythmStart = toCall;
        }

        public void OnRyhthmEnd(Action toCall) {
            _rythmEnd = toCall;
        }
    }
}
