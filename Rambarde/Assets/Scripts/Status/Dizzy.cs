using Characters;

namespace Status {
    class Dizzy : StatusEffect {
        public Dizzy(CharacterControl target, int turns) : base(target, turns) {
            type = EffectType.Dizzy;
        }
    }
}