using Structs;
using UnityEngine;

namespace Skills {
    [CreateAssetMenu(fileName = "Attaque100", menuName = "Skills/Attaque100")]
    public class Atq100Skill : Skill {
        public override void Execute(Stats stats, Character target) {
            target.TakeDamage(stats.atq);
            // Debug.Log(stats.atq + " inflicted to " + target.name + ".\tNew Endurance: " + target.stats.end);
        }
    }
}