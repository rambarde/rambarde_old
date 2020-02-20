using System.Linq;
using Characters;
using Status;
using Structs;
using UnityEngine;

namespace Skills {
    [CreateAssetMenu(fileName = "PoisonSkill", menuName = "Skills/PoisonSkill")]
    public class PoisonSkill : Skill {
        public override void Execute(Stats source, Character target) {
            var team = target.GetTeam();
            foreach (var t in team) {
                var effects = t.StatusEffects;
                var effect = effects.FirstOrDefault(x => x.GetType() == typeof(PoisonEffect));

                if (effect is null) {
                    effects.Add(new PoisonEffect(t, source.atq / 4f, 2));
                }
                else {
                    effect.AddTurns(2);
                }
            }
        }
    }
}