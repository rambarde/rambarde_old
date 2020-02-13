using System.Linq;
using Characters;
using Status;
using Structs;
using UnityEngine;

namespace Skills {
    [CreateAssetMenu(fileName = "PoisonSkill", menuName = "Skills/PoisonSkill")]
    public class PoisonSkill : Skill {
        public override void Execute(Stats source, Character target) {
            var effects = target.StatusEffects;
            var effect = effects.FirstOrDefault(x => x.GetType() == typeof(PoisonEffect));

            if (effect is null) {
                effects.Add(new PoisonEffect(target, source.atq / 2f, 2));
            }
            else {
                effect.AddTurns(2);
            }
        }
    }
}