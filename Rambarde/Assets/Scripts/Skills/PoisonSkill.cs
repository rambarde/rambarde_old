using Status;
using Structs;
using UnityEngine;

namespace Skills {
    [CreateAssetMenu(fileName = "PoisonSkill", menuName = "Skills/PoisonSkill")]
    public class PoisonSkill : Skill {
        public override void Execute(Stats stats, Character target) {
            target.AddEffect(new PoisonEffect(target, stats.atq / 2f));
        }
    }
}