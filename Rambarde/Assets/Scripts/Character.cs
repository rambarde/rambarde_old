using UnityEditor;
using UnityEngine;

public enum Team {
    PlayerTeam = 0,
    EmemyTeam = 1
}

public class Character : MonoBehaviour {
    public Team team;
    public Stats stats;
    public Skill[] skillWheel;
    public CombatManager combatManager;

    private int _skillIndex;
    private Character _target;

    void Start() {
        if (skillWheel.Length == 0) {
            skillWheel = new Skill[] {
                Resources.Load<SayHelloSkill>("ScriptableObjects/SayHelloSkill 1")
            };
        }
    }

    void Update() { }

    public void ExecuteSkill() {
        var skill = skillWheel[_skillIndex];

        _target = skill.ShouldCastOnAllies ? combatManager.GetRandomAlly((int) team) : combatManager.GetRandomEnemy((int) team);
        skill.Execute(_target);
        _skillIndex = (_skillIndex + 1) % skillWheel.Length;
        
        GetComponent<Animator>().SetTrigger("Anim");
    }
}