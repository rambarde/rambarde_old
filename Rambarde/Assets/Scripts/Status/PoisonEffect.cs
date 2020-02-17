using Characters;
using UnityEngine;

namespace Status {
    public class PoisonEffect : StatusEffect {
        private readonly float _dmg;

        public PoisonEffect(Character character, float dmg, int turns) {
            Character = character;
            TurnsLeft = turns;
            _dmg = dmg;
        }

        public override void ApplyEffect() { }

        public override void RemoveEffect() {
            Character.StatusEffects.Remove(this);
        }

        public override void TurnStart() {
            Character.TakeDamage(_dmg);
        }

        protected override void PreTurnEnd() {
            Debug.Log(Character);
        }
    }
}