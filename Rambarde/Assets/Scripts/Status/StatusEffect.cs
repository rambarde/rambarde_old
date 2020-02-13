using System.Collections;
using Characters;

namespace Status {
    // public enum Effect {
        // PoisonEffect,
    // }

    public abstract class StatusEffect {
        // public Effect Effect { get; protected set; }
        
        protected Character Character;
        protected int TurnsLeft;
        private bool _justApplied = true;

        public abstract void ApplyEffect(Character character);

        public abstract void RemoveEffect();

        public abstract void TurnStart();

        public void TurnEnd() {
            if (_justApplied) {
                _justApplied = false;
                return;
            }
            PreTurnEnd();
            PostTurnEnd();
        }

        protected abstract void PreTurnEnd();

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