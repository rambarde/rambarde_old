using System.Collections.Generic;
using Characters;
using UniRx;
using UnityEditor;
using UnityEngine;

namespace Melodies {
    public abstract class Melody : ScriptableObject { 

        [SerializeField] private string data;
        public string Data => data;
        public int Size => data.Length;

        public ReactiveProperty<bool> isPlayable = new ReactiveProperty<bool>();

        public int Tier;
        public int InspirationValue;

        /**
         *  execute melody on targets.
         *  targets can have multiple characters
         */
        public abstract void Execute(CharacterControl target);

    }
}
