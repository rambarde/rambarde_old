using System.Threading.Tasks;
using Characters;
using UnityEngine;

namespace Melodies {
    [CreateAssetMenu(fileName = "DebugMelody", menuName = "Melody/Debug")]
    public class DebugMelody : Melody
    {
        public string message;
        protected override async Task ExecuteOnTarget(CharacterControl t) {
            Debug.Log(message);
        }
    }
}