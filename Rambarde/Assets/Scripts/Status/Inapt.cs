using Characters;

namespace Status {
    class Inapt : StatusEffect {
        public Inapt(CharacterControl target, int turns) : base(target, turns) {
            type = EffectType.Inapt;
        }
    }
}