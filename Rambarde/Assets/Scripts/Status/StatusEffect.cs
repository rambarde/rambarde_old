using System.Collections.Generic;

namespace Status {
    public enum Effect {
        PoisonEffect,
    }

    public abstract class StatusEffect {
        public Effect Effect { get; protected set; }
        
        protected int TurnsLeft;

        public abstract void ApplyEffect(List<StatusEffect> effects);

        public abstract void RemoveEffect();

        public abstract void TurnStart();

        public abstract void TurnEnd();

        public void AddTurns(int n) {
            TurnsLeft += n;
        }
    }
}