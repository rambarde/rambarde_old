using Characters;

namespace Status {
    class Rushing : StatusEffect {
        public Rushing(CharacterControl target, int turns) : base(target, turns) {
            type = EffectType.Rushing;
        }
    }
}