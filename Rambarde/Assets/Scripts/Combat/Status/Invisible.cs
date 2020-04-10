using Characters;

namespace Status {
    class Invisible : StatusEffect {
        public Invisible(CharacterControl target, int turns) : base(target, turns) {
            type = EffectType.Invisible;
        }
    }
}