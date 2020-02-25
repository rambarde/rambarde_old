using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Skills;
using Status;
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
        public ReactiveCollection<StatusEffect> statusEffects;

        protected Animator Animator;
        protected CombatManager CombatManager;

        private int _skillIndex;
        private bool _skillIndexChanged;
        private Character _target;
        private IObservable<int> _animationSkillStateObservable;

        public IEnumerable<Character> GetTeam() {
            return CombatManager.teams[(int) team];
        }

        public async Task EffectsTurnStart() {
            for (var i = statusEffects.Count - 1; i >= 0; --i) {
                var effect = statusEffects[i];
                await effect.TurnStart();
            }
        }

        public async Task ExecTurn() {
            var skill = skillWheel[_skillIndex];
            _skillIndexChanged = false;

            var target = CombatManager.GetRandomChar((int) team, skill.ShouldCastOnAllies);

            // Play and wait for skillAnimation to finish
            await SkillPreHitAnimation(skill.skillName);
            // Execute the skill
            await skill.Execute(stats, target);

            // Apply effects at the end of the turn
            for (var i = statusEffects.Count - 1; i >= 0; --i) {
                await statusEffects[i].TurnEnd();
            }

            // Increment skillIndex ONLY if effects did not affect the wheel
            if (!_skillIndexChanged)
                _skillIndex = (_skillIndex + 1) % skillWheel.Length;
        }

        private async Task SkillPreHitAnimation(string skillName) {
            Animator.SetTrigger(skillName);
            var stateHash = Animator.StringToHash(skillName);

            await Utils.AwaitObservable(
                this.UpdateAsObservable()
                    .SkipWhile(_ => !Animator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(stateHash))
                    .TakeWhile(_ => Animator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(stateHash))
            );
        }


        public async Task Heal(float pts) {
            var newHealth = stats.hp.Value + pts;
            if (newHealth < stats.maxHp)
                stats.hp.Value = newHealth;
            
            await Utils.AwaitObservable(Observable.Timer(TimeSpan.FromSeconds(1)));
        }

        private float CalculateDamage(float dmg) {
            var curEnd = stats.hp.Value;
            curEnd -= dmg * (1 - stats.prot / 100f);
            if (curEnd < 0) {
                curEnd = 0;
            }

            return curEnd;
        }

        public async Task TakeDamage(float dmg) {
            stats.hp.Value = CalculateDamage(dmg);
            if (stats.hp.Value > 0) return;

            // TODO: spawn floating text for damage taken
            // TODO: Change this to wait for animation take damage finish
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
            statusEffects = new ReactiveCollection<StatusEffect>();
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