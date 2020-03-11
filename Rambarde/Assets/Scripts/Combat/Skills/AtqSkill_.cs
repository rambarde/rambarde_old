using System.Threading.Tasks;
using Characters;
using UnityEngine;

namespace Skills {
    [CreateAssetMenu(fileName = "Atq", menuName = "Skills/Atq")]
    public class AtqSkill_ : Skill_ {
        public override async Task Execute(Stats source, CharacterControl target) {
            if (Random.Range(0, 100) <= source.prec) {
                var dmg = Random.Range(0, 100) <= source.crit ? source.atq * 2 : source.atq;
                // Debug.Log("Damage inflicted: " + dmg);
                await target.TakeDamage(dmg);
            }
            else Debug.Log("Miss!");
        }
    }
}