using System.Collections.Generic;
using Characters;
using UnityEngine;

namespace Melody {
    [CreateAssetMenu(fileName = "DebugMelody", menuName = "Melody/DebugMelody")]
    
    public class DebugMelody : Melody {
        public override void Execute(List<Character> targets) {
            Debug.Log("You played a melody.");
        }
    }
}