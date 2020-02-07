using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Status {
    public class PoisonEffect : StatusEffect {
        private readonly Character _character;
        private float _dmg;

        public PoisonEffect(Character character, float dmg) {
            TurnsLeft = 2;
            _character = character;
            _dmg = dmg;
            Effect = Effect.PoisonEffect;
        }

        public override void ApplyEffect(List<StatusEffect> effects) {
            StatusEffect effect = null;
            foreach (var seffect in effects) {
                if (seffect.Effect == Effect.PoisonEffect) {
                    effect = seffect;
                }
            }

            if (effect is null) {
               effects.Add(this); 
            }
            else {
                effect.AddTurns(TurnsLeft);
            }
        }

        public override void RemoveEffect() {
            Debug.Log(_character.name + " is no longer poisoned!");
        }

        public override void TurnStart() {
            _character.TakeDamage(_dmg);
        }

        public override void TurnEnd() { }
    }
}