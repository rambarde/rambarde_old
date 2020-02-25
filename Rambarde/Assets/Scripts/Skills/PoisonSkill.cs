using System;
using System.Linq;
using System.Threading.Tasks;
using Characters;
using Status;
using UnityEngine;

namespace Skills {
    [CreateAssetMenu(fileName = "PoisonSkill", menuName = "Skills/PoisonSkill")]
    public class PoisonSkill : Skill {
        public override async Task Execute(Stats source, Character target) {
            foreach (var t in target.GetTeam()) {
                await StatusEffect.ApplyEffect(
                    t,
                    new Lazy<PoisonEffect>(() => new PoisonEffect(t, source.atq / 4f, 2)),
                    2
                );
            }
        }
    }
}