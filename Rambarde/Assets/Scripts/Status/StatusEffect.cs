using System;
using System.Collections;
using System.Threading.Tasks;
using Characters;

namespace Status {
    [Serializable]
    public abstract class StatusEffect : IStatusEffect {
        protected Character Target;
        protected int TurnsLeft;
        private bool _justApplied = true;

        // Abstract methods
        public abstract void ApplyEffect();

        public abstract void RemoveEffect();

        public abstract Task TurnStart();

        protected abstract Task PreTurnEnd();

        // Final methods
        public async Task TurnEnd() {
            if (_justApplied) {
                _justApplied = false;
                return;
            }

            await PreTurnEnd();
            PostTurnEnd();
        }

        private void PostTurnEnd() {
            if (--TurnsLeft == 0) {
                RemoveEffect();
            }
        }

        public void AddTurns(int n) {
            TurnsLeft += n;
        }
    }
}