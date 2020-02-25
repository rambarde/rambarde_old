using System;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using Characters;
using UniRx;
using UnityEngine;

namespace Status {
    [Serializable]
    public abstract class StatusEffect {
        public ReactiveProperty<int> turnsLeft;
        public string spriteName;

        protected Character Target;

        private bool _justApplied = true;

        // Abstract methods
        protected virtual async Task PreTurnStart() { }

        protected virtual async Task PostTurnEnd() { }

        // Final methods
        public async Task TurnStart() {
            await PreTurnStart();

            if (--turnsLeft.Value == 0) {
                RemoveEffect();
            }
        }

        public async Task TurnEnd() {
            if (_justApplied) {
                _justApplied = false;
                return;
            }

            await PostTurnEnd();
        }

        public void RemoveEffect() {
            Target.statusEffects.Remove(this);
        }

        public static async Task ApplyEffect<T>(Character target, Lazy<T> addedEffect, int addedTurns = 0) where T : StatusEffect {
            // TODO: applying effect animation

            var effects = target.statusEffects;
            var effect = effects.FirstOrDefault(x => x.GetType() == typeof(T));

            if (effect is null) {
                effects.Add(addedEffect.Value);
            }
            else {
                effect.AddTurns(addedTurns);
            }
        }

        public void AddTurns(int n) {
            turnsLeft.Value += n;
        }
    }
}