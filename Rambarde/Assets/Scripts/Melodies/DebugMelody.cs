using System.Threading.Tasks;
using Characters;
using UnityEngine;

namespace Melodies {
    [CreateAssetMenu(fileName = "DebugMelody", menuName = "Melody/Debug")]
    public class DebugMelody : Melody
    {
        public string message;
        public override async Task Execute() {
            Debug.Log(message);
        }
    }
}