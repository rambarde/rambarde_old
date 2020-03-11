using System;
using System.Threading.Tasks;
using Characters;
using Status;
using UnityEngine;

namespace Skills {
    [CreateAssetMenu(fileName = "PoisonSkill", menuName = "Skills/PoisonSkill")]
    public class PoisonSkill_ : Skill_ {
        [SerializeField] private int turns;
        [SerializeField] private float atqRatio;

        public override async Task Execute(Stats source, CharacterControl target) {
            await StatusEffect.ApplyEffect(
                target,
                new Lazy<PoisonEffect>(() => new PoisonEffect(target, source.atq / atqRatio, turns)),
                turns
            );
        }
    }
}