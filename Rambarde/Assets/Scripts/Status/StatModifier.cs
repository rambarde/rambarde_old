using System.Threading.Tasks;
using Characters;

namespace Status {
    public enum ModifierStrength {
        Buff1,
        Buff2,
        Buff3,
        Debuff1,
        Debuff2,
        Debuff3
    }
    
    public abstract class StatModifier : StatusEffect {
        protected float modifiedValue;

        protected StatModifier(CharacterControl target, int turns) : base(target, turns) { }
    }
}