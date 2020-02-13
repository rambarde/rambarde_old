namespace Status {
    public interface IStatusEffect {
        void ApplyEffect();
        void RemoveEffect();
        void TurnEnd();
        void TurnStart();
        void AddTurns(int n);
    }
}