using System.Collections;

namespace Status {
    public interface IStatusEffect {
        void ApplyEffect();
        void RemoveEffect();
        void TurnEnd();
        IEnumerator TurnStart();
        void AddTurns(int n);
    }
}