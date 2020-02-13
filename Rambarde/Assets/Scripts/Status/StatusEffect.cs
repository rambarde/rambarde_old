using System.Collections;
using Characters;
using Skills;

namespace Status {
    public abstract class StatusEffect : IStatusEffect {
        protected Character Character;
        protected int TurnsLeft;
        private bool _justApplied = true;

        // Abstract methods
        public abstract void ApplyEffect();

        public abstract void RemoveEffect();

        public abstract void TurnStart();

        protected abstract void PreTurnEnd();

        // Final methods
        public void TurnEnd() {
            if (_justApplied) {
                _justApplied = false;
                return;
            }

            PreTurnEnd();
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