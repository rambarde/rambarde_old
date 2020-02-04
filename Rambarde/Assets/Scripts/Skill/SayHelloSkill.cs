using UnityEngine;


[CreateAssetMenu(fileName = "SayHelloSkill", menuName = "Skills/SayHelloSkill")]
public class SayHelloSkill : Skill {
    public string msg;

    public override void Execute(Character target) {
        Debug.Log(msg + " " + target);
        msg = "qwe";
    }
}