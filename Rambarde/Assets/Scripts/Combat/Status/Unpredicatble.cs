using Characters;

namespace Status {
    class Unpredictable : StatusEffect {
        public Unpredictable(CharacterControl target, int turns) : base(target, turns) {
            type = EffectType.Unpredictable;
        }
    }
}