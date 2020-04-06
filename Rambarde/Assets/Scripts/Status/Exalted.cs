using Characters;

namespace Status {
    class Exalted : StatusEffect {
        public Exalted(CharacterControl target, int turns) : base(target, turns) {
            type = EffectType.Exalted;
        }
    }
}