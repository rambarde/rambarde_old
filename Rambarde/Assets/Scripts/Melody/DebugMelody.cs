using System.Collections.Generic;
using Characters;
using UnityEngine;

namespace Melody {
    public class DebugMelody : Melody {
        public override void Execute(List<Character> targets) {
            Debug.Log("You played a melody.");
        }
    }
}