using System.Linq;
using Characters;
using UnityEngine;

namespace Status {
    public class PoisonEffect : StatusEffect {
        private readonly float _dmg;

        public PoisonEffect(float dmg) {
            TurnsLeft = 2;
            _dmg = dmg;
        }

        public override void ApplyEffect(Character character) {
            var effects = character.StatusEffects;
            Character = character;
            
            var effect = effects.FirstOrDefault(x => x.GetType() == typeof(PoisonEffect));

            if (effect is null) {
               effects.Add(this); 
            }
            else {
                effect.AddTurns(TurnsLeft);
            }
        }

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