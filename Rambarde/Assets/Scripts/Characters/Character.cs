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
        protected CombatManager CombatManager;

        private int _skillIndex;
        private bool _skillIndexChanged;
        private Character _target;

        public IEnumerator ExecuteSkill() {
            var skill = skillWheel[_skillIndex];
            _skillIndexChanged = false;

            _target = skill.ShouldCastOnAllies ? CombatManager.GetRandomAlly((int) team) : CombatManager.GetRandomEnemy((int) team);
            skill.Execute(stats, _target);

            for (int i = StatusEffects.Count - 1; i >= 0; --i) {
                StatusEffects[i].TurnEnd();
            }

            if (!_skillIndexChanged)
                _skillIndex = (_skillIndex + 1) % skillWheel.Length;

            Animator.SetTrigger(skill.skillName);
            yield return new WaitUntil(() => {
                var time = Animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
                Debug.Log(time - (int) time);
                return time - (int) time >= 0.8f;
            });
        }

        public void TakeDamage(float dmg) {
            stats.end -= dmg;
            if (stats.end <= 0) {
                CombatManager.Remove(this);
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
            CombatManager = CombatManager.Instance;
            Debug.Log("VAR");
            Debug.Log(CombatManager);
            
            if (skillWheel.Length == 0) {
                skillWheel = new Skill[] {
                    Resources.Load<SayHelloSkill>("ScriptableObjects/SayHelloSkill 1")
                };
            }
        }

        #endregion
    }
}