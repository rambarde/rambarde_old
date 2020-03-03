using System;
using System.Linq;
using System.Threading.Tasks;
using Characters;
using UniRx;

namespace Status {
    [Serializable]
    public abstract class StatusEffect {
        public ReactiveProperty<int> turnsLeft;
        public string spriteName;

        protected CharacterControl target;

        private bool _justApplied = true;

        protected StatusEffect(CharacterControl target, int turns) {
            this.target = target;
            turnsLeft = new ReactiveProperty<int>(turns);
        }

        // Abstract methods
        protected virtual async Task PreTurnStart() { }

        protected virtual async Task PostTurnEnd() { }

        protected virtual void Cleanup() { }

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
            Cleanup();
            target.statusEffects.Remove(this);
        }

        public static async Task ApplyEffect<T>(CharacterControl target, Lazy<T> addedEffect, int addedTurns = 0) where T : StatusEffect {
            //TODO: applying effect animation

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