using Characters;
using Structs;
using UnityEngine;

namespace Skills {
    public class EmptySkill : Skill {
        public override void Execute(Stats source, Character target) {
            // Debug.Log("Empty skill executing");
        }
    }
}