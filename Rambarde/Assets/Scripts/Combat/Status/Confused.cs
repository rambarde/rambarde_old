using Characters;

namespace Status {
    class Confused : StatusEffect {
        public Confused(CharacterControl target, int turns) : base(target, turns) {
            type = EffectType.Confused;
        }
    }
}