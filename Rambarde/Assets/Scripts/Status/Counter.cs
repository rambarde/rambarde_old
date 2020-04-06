using Characters;

namespace Status {
    class Counter : StatusEffect {
        public Counter(CharacterControl target, int turns) : base(target, turns) {
            type = EffectType.Counter;
        }
    }
}