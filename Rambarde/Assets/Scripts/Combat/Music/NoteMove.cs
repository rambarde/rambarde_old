using UnityEngine;

namespace Music {
    public class NoteMove : MonoBehaviour {

        public float speed = 2f;
        
        void FixedUpdate() {
            transform.position += Vector3.left * (Time.fixedDeltaTime * speed);
        }
    }
}
