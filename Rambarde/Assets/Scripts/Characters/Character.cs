using System;
using System.Collections.Generic;
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
        private CharacterVfx _vfx;

        public List<Character> GetTeam() {
            return CombatManager.teams[(int) team];
        }

        public async Task EffectsTurnStart() {
            for (var i = StatusEffects.Count - 1; i >= 0; --i) {
                var effect = StatusEffects[i];
                await effect.TurnStart();
            }
        }

        public async Task ExecTurn() {
            var skill = skillWheel[_skillIndex];
            _skillIndexChanged = false;

            var target = CombatManager.GetRandomChar((int) team, skill.ShouldCastOnAllies);
            
            // Play and wait for skillAnimation to finish
            await SkillAnimation(skill.skillName);
            // Execute the skill
            await skill.Execute(stats, target);

            // Apply effects at the end of the turn
            for (var i = StatusEffects.Count - 1; i >= 0; --i) {
                await StatusEffects[i].TurnEnd();
            }

            // Increment skillIndex ONLY if effects did not affect the wheel
            if (!_skillIndexChanged)
                _skillIndex = (_skillIndex + 1) % skillWheel.Length;
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

        private float CalculateDamage(float dmg) {
            var curEnd = stats.end.Value;
            curEnd -= dmg * (1 - stats.prot / 100f);
            if (curEnd <= 0) {
                // DeathFlag = true;
                curEnd = 0;
            }

            return curEnd;
        }

        public async Task TakeDamage(float dmg) {
            stats.end.Value = CalculateDamage(dmg);
            if (stats.end.Value > 0) return;
            
            await Utils.AwaitObservable(Observable.Timer(TimeSpan.FromSeconds(1)));
            CombatManager.Remove(this);
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
            stats.Init();
        }

        protected void Start() {
            CombatManager = CombatManager.Instance;

            // if (skillWheel.Length == 0) {
            //     skillWheel = new Skill[] {
            //         ScriptableObject.CreateInstance<EmptySkill>()
            //     };
            // }
        }

        #endregion
    }
}