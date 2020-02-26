using System.Threading.Tasks;
using Characters;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Melodies {
    public abstract class Melody : ScriptableObject { 

        [SerializeField] private string data;
        public string Data => data;
        public int Size => data.Length;

        public ReactiveProperty<bool> isPlayable = new ReactiveProperty<bool>(true);

        public int tier;
        public int inspirationValue;

        /**
         *  execute melody on targets.
         *  targets can have multiple characters
         */
        public abstract Task Execute(CharacterControl target);

    }
}
