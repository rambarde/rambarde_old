using UnityEngine;

public class EmptySkill : Skill {
    public override void Execute(Character target) {
        Debug.Log("Empty skill executing");
    }
}