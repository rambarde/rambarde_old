using System.Collections;
using System.Collections.Generic;
using Skills;
using Status;
using Structs;
using UnityEngine;

namespace Characters {
    public enum Team {
        PlayerTeam = 0,
        EmemyTeam = 1
    }

    public abstract class Character : MonoBehaviour {
        public Team team;
        public Stats stats;
        public Skill[] skillWheel;

        public List<StatusEffect> StatusEffects { get; private set; }
        protected Animator Animator;

        private int _skillIndex;
        private bool _skillIndexChanged;
        private Character _target;
        protected CombatManager _combatManager;
        private static readonly int Skill = Animator.StringToHash("Skill");

        public void ExecuteSkill() {
            var skill = skillWheel[_skillIndex];
            _skillIndexChanged = false;

            _target = skill.ShouldCastOnAllies ? _combatManager.GetRandomAlly((int) team) : _combatManager.GetRandomEnemy((int) team);
            skill.Execute(stats, _target);

            for (int i = StatusEffects.Count - 1; i >= 0; --i) {
                StatusEffects[i].TurnEnd();
            }

            if (!_skillIndexChanged)
                _skillIndex = (_skillIndex + 1) % skillWheel.Length;

            Animator.SetTrigger(Skill);
        }

        public void TakeDamage(float dmg) {
            stats.end -= dmg;
            if (stats.end <= 0) {
                _combatManager.Remove(this);
            }
        }

        public void IncrementSkillWheel() {
            _skillIndex++;
            _skillIndexChanged = true;
        }

        public void DecrementSkillWheel() {
            _skillIndex--;
            _skillIndexChanged = true;
        }

        #region Unity

        private void Awake() {
            StatusEffects = new List<StatusEffect>();
        }

        void Start() {
            _combatManager = CombatManager.Instance;
            Debug.Log("VAR");
            Debug.Log(_combatManager);
            
            if (skillWheel.Length == 0) {
                skillWheel = new Skill[] {
                    Resources.Load<SayHelloSkill>("ScriptableObjects/SayHelloSkill 1")
                };
            }
        }

        #endregion
    }
}