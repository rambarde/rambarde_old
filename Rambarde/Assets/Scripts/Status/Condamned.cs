using Characters;

namespace Status {
    class Condemned : StatusEffect {
        public Condemned(CharacterControl target, int turns) : base(target, turns) {
            type = EffectType.Condemned;
        }
    }
}