using Characters;

namespace Status {
    // public enum Effect {
        // PoisonEffect,
    // }

    public abstract class StatusEffect {
        // public Effect Effect { get; protected set; }
        
        protected int TurnsLeft;

        public abstract void ApplyEffect(Character character);

        public abstract void RemoveEffect();

        public abstract void TurnStart();

        public virtual void TurnEnd() {
            if (--TurnsLeft == 0) {
                RemoveEffect();
            }
        }

        public void AddTurns(int n) {
            TurnsLeft += n;
        }
    }
}