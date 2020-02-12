using Characters;
using UnityEngine;

namespace Status {
    public class PoisonEffect : StatusEffect {
        private float _dmg;

        public PoisonEffect(float dmg) {
            TurnsLeft = 2;
            _dmg = dmg;
            // Effect = Effect.PoisonEffect;
        }

        public override void ApplyEffect(Character character) {
            var effects = character.StatusEffects;
            Character = character;
            
            StatusEffect effect = null;
            foreach (var seffect in effects) {
                if (seffect.GetType() == typeof(PoisonEffect)) {
                    effect = seffect;
                }
            }

            if (effect is null) {
               effects.Add(this); 
            }
            else {
                effect.AddTurns(TurnsLeft);
            }

            // Debug.Log(character.name + " has been poisoned!");
        }

        public override void RemoveEffect() {
            Character.StatusEffects.Remove(this);
            // Debug.Log("Poison effect removed from " + _character.name);
        }

        public override void TurnStart() {
            Character.TakeDamage(_dmg);
        }

        public override void TurnEnd() {
            base.TurnEnd();
        }
    }
}