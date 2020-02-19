using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Skills;
using Status;
using Structs;
using UniRx;
using UniRx.Triggers;
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
        public List<IStatusEffect> StatusEffects { get; private set; }

        protected Animator Animator;
        protected CombatManager CombatManager;

        private int _skillIndex;
        private bool _skillIndexChanged;
        private Character _target;
        private IObservable<int> _animationSkillStateObservable;

        public List<Character> GetTeam() {
            return CombatManager.teams[(int) team];
        }

        public async Task ExecuteSkill() {
            var skill = skillWheel[_skillIndex];
            _skillIndexChanged = false;

            _target = skill.ShouldCastOnAllies ? CombatManager.GetRandomAlly((int) team) : CombatManager.GetRandomEnemy((int) team);
            Debug.Log(name + " is attacking " + _target.name + " with " + skill.skillName);
            skill.Execute(stats, _target);

            for (int i = StatusEffects.Count - 1; i >= 0; --i) {
                StatusEffects[i].TurnEnd();
            }

            if (!_skillIndexChanged)
                _skillIndex = (_skillIndex + 1) % skillWheel.Length;

            await SkillAnimation(skill.skillName);
        }

        private async Task SkillAnimation(string skillName) {
            Animator.SetTrigger(skillName);
            var stateHash = Animator.StringToHash(skillName);

            await Utils.AwaitObservable(
                this.UpdateAsObservable()
                    .SkipWhile(_ => !Animator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(stateHash))
                    .TakeWhile(_ => Animator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(stateHash))
            );
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
            StatusEffects = new List<IStatusEffect>();
        }

        protected void Start() {
            CombatManager = CombatManager.Instance;

            if (skillWheel.Length == 0) {
                skillWheel = new Skill[] {
                    ScriptableObject.CreateInstance<EmptySkill>()
                };
            }
        }

        #endregion
    }
}