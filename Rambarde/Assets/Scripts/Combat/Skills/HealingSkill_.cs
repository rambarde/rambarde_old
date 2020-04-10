using System;
using System.Threading.Tasks;
using Characters;
using Status;
using UnityEngine;

namespace Skills {
    [CreateAssetMenu(fileName = "HealingSkill", menuName = "Skills/HealingSkill")]
    public class HealingSkill_ : Skill_ {
        [SerializeField] private int turns;
        [SerializeField] private float atqRatio;

        public override async Task Execute(Stats source, CharacterControl target) {
            foreach (var t in target.GetTeam()) {
                await StatusEffect.ApplyEffect(t, new Lazy<HealthRegen>(() => new HealthRegen(t, source.atq / atqRatio, turns)));
            }
        }
    }
}