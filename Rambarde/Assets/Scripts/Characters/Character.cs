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
        public CombatManager combatManager;

        public List<StatusEffect> StatusEffects { get; private set; }
        protected Animator animator;

        private int _skillIndex;
        private bool _skillIndexChanged;
        private Character _target;
        private static readonly int Skill = Animator.StringToHash("Skill");

        private bool _animationEnded;

        public void ExecuteSkill() {
            var skill = skillWheel[_skillIndex];
            _skillIndexChanged = false;

            _target = skill.ShouldCastOnAllies ? combatManager.GetRandomAlly((int) team) : combatManager.GetRandomEnemy((int) team);
            skill.Execute(stats, _target);

            for (int i = StatusEffects.Count - 1; i >= 0; --i) {
                StatusEffects[i].TurnEnd();
            }

            if (!_skillIndexChanged)
                _skillIndex = (_skillIndex + 1) % skillWheel.Length;

            animator.SetTrigger(Skill);
        }

        public void AnimationDone(string message) {
            _animationEnded = true;
        }

        public void TakeDamage(float dmg) {
            stats.end -= dmg;
            if (stats.end <= 0) {
                combatManager.Remove(this);
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
            if (skillWheel.Length == 0) {
                skillWheel = new Skill[] {
                    Resources.Load<SayHelloSkill>("ScriptableObjects/SayHelloSkill 1")
                };
            }
        }

        #endregion
    }
}