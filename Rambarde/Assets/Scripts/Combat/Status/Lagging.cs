using Characters;

namespace Status {
    class Lagging : StatusEffect {
        public Lagging(CharacterControl target, int turns) : base(target, turns) {
            type = EffectType.Lagging;
        }
    }
}