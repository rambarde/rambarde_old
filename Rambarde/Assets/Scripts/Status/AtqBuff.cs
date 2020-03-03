using System.ComponentModel;
using System.Threading.Tasks;
using Characters;
using UniRx;
using UnityEngine;

namespace Status {
    public class AtqBuff : StatModifier {
        public AtqBuff(CharacterControl target, int turns, ModifierStrength mod) : base(target, turns) {
            var oldStat = target.characterData.baseStats.atq;

            modifiedValue = mod switch {
                ModifierStrength.Buff1 => oldStat * 1.3f,
                ModifierStrength.Buff2 => oldStat * 1.4f,
                ModifierStrength.Buff3 => oldStat * 1.5f,
                ModifierStrength.Debuff1 => oldStat * 0.75f,
                ModifierStrength.Debuff2 => oldStat * 0.65f,
                ModifierStrength.Debuff3 => oldStat * 0.6f,
                _ => throw new InvalidEnumArgumentException($"{mod} is not a valid enum.") 
            };

            target.currentStats.atq += modifiedValue;
        }

        protected override void Cleanup() {
            target.currentStats.atq -= modifiedValue;
        }
    }
}