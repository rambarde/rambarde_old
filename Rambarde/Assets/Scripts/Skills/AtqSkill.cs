using Characters;
using Structs;
using UnityEngine;

namespace Skills {
    [CreateAssetMenu(fileName = "Atq", menuName = "Skills/Atq")]
    public class AtqSkill : Skill {
        public override void Execute(Stats source, Character target) {
            if (Random.Range(0, 100) <= source.crit) {
                var dmg = Random.Range(0, 100) <= source.crit ? source.atq * 2 : source.atq;
                Debug.Log("Damage inflicted: " + dmg);
                target.TakeDamage(dmg);
            }
            else Debug.Log("Miss!");
        }
    }
}