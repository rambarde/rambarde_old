using Characters;

namespace Status {
    class Marked : StatusEffect {
        public Marked(CharacterControl target, int turns) : base(target, turns) {
            type = EffectType.Marked;
        }
    }
}