﻿using System.Collections.Generic;
using Characters;
using UnityEngine;

namespace Melodies {
    [CreateAssetMenu(fileName = "DebugMelody", menuName = "Melody/Debug")]
    public class DebugMelody : Melody {
        public override void Execute(Character target) {
            Debug.Log("You played a melody.");
        }
    }
}