using Characters;
using Structs;
using UnityEngine;

namespace Skills {
    [CreateAssetMenu(fileName = "SayHelloSkill", menuName = "Skills/SayHelloSkill")]
    public class SayHelloSkill : Skill {
        public string msg;

        public override void Execute(Stats source, Character target) {
            Debug.Log(msg + ": " + source + " " + target);
        }
    }
}