using System.Collections.Generic;
using System.Diagnostics;
using Characters;
using Debug = UnityEngine.Debug;

namespace Melodies {
    public class PrestoMelody : Melody {
        public override void Execute(List<Character> targets) {
            if (targets.Count != 1) {
                Debug.LogWarning("Presto cannot be applied on " + targets.Count + " target(s).");
                return;
            }
            
            targets[0].IncrementSkillWheel();
        }
    }
}