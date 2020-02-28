using System.Threading.Tasks;
using Bard;
using Characters;
using JetBrains.Annotations;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace Melodies {
    public abstract class Melody : ScriptableObject {
        [SerializeField] private string data;
        public string Data => data;
        public int Size => data.Length;
        
        public MelodyTargetMode targetMode;
        
        [System.NonSerialized] public CharacterControl target = null;
        [System.NonSerialized] public ReactiveProperty<bool> isPlayable = new ReactiveProperty<bool>(true);

        public int tier;
        public int inspirationValue;

        public abstract Task Execute();
        
    }
}