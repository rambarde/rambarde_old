using Characters;

namespace Status {
    class Disciplined : StatusEffect {
        public Disciplined(CharacterControl target, int turns) : base(target, turns) {
            type = EffectType.Disciplined;
        }
    }
}