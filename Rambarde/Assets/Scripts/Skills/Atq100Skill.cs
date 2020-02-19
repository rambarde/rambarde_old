using Characters;
using Structs;
using UnityEngine;

namespace Skills {
    [CreateAssetMenu(fileName = "Attaque100", menuName = "Skills/Attaque100")]
    public class Atq100Skill : Skill {
        public override void Execute(Stats source, Character target) {
            Debug.Log("Damage inflicted: " + source.atq);
            target.TakeDamage(source.atq);
        }
    }
}