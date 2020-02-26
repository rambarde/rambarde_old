using System.Collections.Generic;
using System.Diagnostics;
using Characters;
using Debug = UnityEngine.Debug;

namespace Melodies {
    public class PrestoMelody : Melody {
        public override void Execute(CharacterControl target) {
            target.IncrementSkillWheel();
        }
    }
}