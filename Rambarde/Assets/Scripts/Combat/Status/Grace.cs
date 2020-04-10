using Characters;

namespace Status {
    class Grace : StatusEffect {
        public Grace(CharacterControl target, int turns) : base(target, turns) {
            type = EffectType.Grace;
        }
    }
}