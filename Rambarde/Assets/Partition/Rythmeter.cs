using System.Collections;
using UnityEngine;

namespace Partition {
    public class Rythmeter : MonoBehaviour {

        public float distance;
        public float duration;

        private void Start() {
            StartCoroutine(nameof(MoveRythm));
        }

        private IEnumerator MoveRythm() {
            float speed = distance / duration;
            float walked = 0;
            while (walked < distance) {
                var tf = transform;
                tf.position += new Vector3(Time.deltaTime * speed, 0, 0);
                walked += Time.deltaTime * speed;
                yield return null;
            }
        }
    }
}
