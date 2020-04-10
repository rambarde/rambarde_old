using Characters;

namespace Status {
    class Merciless : StatusEffect {
        public Merciless(CharacterControl target, int turns) : base(target, turns) {
            type = EffectType.Merciless;
        }
    }
}