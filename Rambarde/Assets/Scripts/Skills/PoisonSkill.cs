using System.Linq;
using System.Threading.Tasks;
using Characters;
using Status;
using Structs;
using UnityEngine;

namespace Skills {
    [CreateAssetMenu(fileName = "PoisonSkill", menuName = "Skills/PoisonSkill")]
    public class PoisonSkill : Skill {
        public override async Task Execute(Stats source, Character target) {
            foreach (var t in target.GetTeam()) {
                var effects = t.statusEffects;
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