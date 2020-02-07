using System.Collections.Generic;
using Skills;
using Structs;
using UnityEngine;

using Status;

public enum Team {
    PlayerTeam = 0,
    EmemyTeam = 1
}

public class Character : MonoBehaviour {
    public Team team;
    public Stats stats;
    public Skill[] skillWheel;
    public CombatManager combatManager;

    public List<StatusEffect> StatusEffects { get; private set; }

    private int _skillIndex;
    private bool _skillIndexChanged;
    private Character _target;


    public void ExecuteSkill() {
        var skill = skillWheel[_skillIndex];
        _skillIndexChanged = false;

        _target = skill.ShouldCastOnAllies ? combatManager.GetRandomAlly((int) team) : combatManager.GetRandomEnemy((int) team);
        // Debug.Log(name + " executing skill:");
        skill.Execute(stats, _target);

        foreach (var effect in StatusEffects) {
            effect.TurnEnd();
        }
        
        if (!_skillIndexChanged)
            _skillIndex = (_skillIndex + 1) % skillWheel.Length;
    }

    public void TakeDamage(float dmg) {
        stats.end -= dmg;
    }

    public void IncrementSkillWheel() {
        _skillIndex++;
        _skillIndexChanged = true;
    }

    public void DecrementSkillWheel() {
        _skillIndex--;
        _skillIndexChanged = true;
    }

    public void AddEffect(StatusEffect effect) {
        effect.ApplyEffect(StatusEffects);
    }

    public void RemoveEffect(StatusEffect effect) {
        StatusEffects.Remove(effect);
    }

    #region Unity

    private void Awake() {
        StatusEffects = new List<StatusEffect>();
    }

    void Start() {
        if (skillWheel.Length == 0) {
            skillWheel = new Skill[] {
                Resources.Load<SayHelloSkill>("ScriptableObjects/SayHelloSkill 1")
            };
        }
    }

    #endregion
}