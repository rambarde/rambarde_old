using Characters;
using Status;
using Structs;
using UnityEngine;

namespace Skills {
    [CreateAssetMenu(fileName = "PoisonSkill", menuName = "Skills/PoisonSkill")]
    public class PoisonSkill : Skill {
        public override void Execute(Stats source, Character target) {
            var effect = new PoisonEffect(source.atq / 2f);
            effect.ApplyEffect(target);
        }
    }
}