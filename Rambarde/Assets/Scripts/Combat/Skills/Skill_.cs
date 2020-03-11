using System.Threading.Tasks;
using Characters;
using UnityEngine;

namespace Skills {
    public abstract class Skill_ : ScriptableObject {
        [SerializeField] private bool shouldCastOnAllies;
        public bool ShouldCastOnAllies => shouldCastOnAllies;

        public abstract Task Execute(Stats source, CharacterControl target);

        // String used for animation triggers and animation states
        public string animationName;

        // Sprite for UI in skill wheel and clients menu
        public Sprite sprite;

        [TextArea] public string description;
    }
}