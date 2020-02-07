using Structs;
using UnityEngine;

namespace Skills {
    public abstract class Skill : ScriptableObject {
        [SerializeField] private bool shouldCastOnAllies;

        public bool ShouldCastOnAllies => shouldCastOnAllies;

        public abstract void Execute(Stats stats, Character target);

        public string Name;

        public Sprite sprite;
    }
}