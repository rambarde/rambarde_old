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

    void Start() { }

    void Update() { }

    public void ExecuteAbility() {
        var skill = skillWheel[_skillIndex];
        
        _target = skill.ShouldCastOnAllies ? combatManager.GetRandomAlly((int) team) : combatManager.GetRandomEnemy((int) team);
        skill.Execute(_target);
        _skillIndex = (_skillIndex + 1) % skillWheel.Length;
    }
}