using Melodies;
using UnityEngine;

namespace Music {
    public class Note : MonoBehaviour {
        public int note;
        public Melody melody;
        private bool _played;

        public bool Played => _played;

        public void Play() {
            melody.score.Value += 1;
            _played = true;
        }
    }
}
