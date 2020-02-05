using System.Collections;
using UnityEngine;

namespace Partition
{
    public class MusicInput : MonoBehaviour
    {

        public GameObject rythmeter;
        
        void Start()
        {
            StartCoroutine("MoveRythm");
        }

        IEnumerator MoveRythm() {
            Transform tf = rythmeter.transform;
            while (tf.position.x < 7)
            {
                tf.position = tf.position + new Vector3(Time.deltaTime * 1.0f, 0, 0);
                yield return null;
            }
        }
    }
}
