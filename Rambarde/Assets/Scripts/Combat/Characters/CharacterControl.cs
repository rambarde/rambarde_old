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

    public class CharacterControl : MonoBehaviour {
        public String clientName;
        public Team team;
        public Skill[] skillWheel;
        public Stats currentStats;
        public Equipment[] equipment;
        public CharacterData characterData;
        public ReactiveCollection<StatusEffect> statusEffects;
        public ReactiveProperty<EffectType> effectTypes;
        
        private int _skillIndex;
        private bool _skillIndexChanged;
        private Animator _animator;
        private CombatManager _combatManager;
        //TODO make target modifiable through CombatManager/melodies
        private CharacterControl _target;
        private IObservable<int> _animationSkillStateObservable;

        #region Turn

        public IEnumerable<CharacterControl> GetTeam() {
            return _combatManager.teams[(int) team];
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

            // Play and wait for skillAnimation to finish
            await SkillPreHitAnimation(skill.animationName);
            // Execute the skill
            await skill.Execute(this);

            // Apply effects at the end of the turn
            for (var i = statusEffects.Count - 1; i >= 0; --i) {
                await statusEffects[i].TurnEnd();
            }

            // Increment skillIndex ONLY if effects did not affect the wheel
            if (!_skillIndexChanged)
                await IncrementSkillWheel();
        }

        private async Task SkillPreHitAnimation(string animationName) {
            _animator.SetTrigger(animationName);
            var stateHash = Animator.StringToHash(animationName);

            await Utils.AwaitObservable(
                this.UpdateAsObservable()
                    .SkipWhile(_ => !_animator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(stateHash))
                    .TakeWhile(_ => _animator.GetCurrentAnimatorStateInfo(0).shortNameHash.Equals(stateHash))
            );
        }

        #endregion

        #region CharacterData

        public void Init(CharacterData data) {
            characterData = data;
            data.Init();
            //data.equipments.ObserveCountChanged().Subscribe(_ => UpdateStats(0)).AddTo(this);
            //data.weapons.ObserveCountChanged().Subscribe(_ => UpdateStats(1)).AddTo(this);
            skillWheel = data.skills;

            UpdateStats();
            GetComponentInChildren<CharacterVfx>().Init();
        }

        public void UpdateStats() {
            // var stats = characterData.baseStats;
            // switch (accessory) {
            //     case 0:
            //         foreach (var armor in characterData.armors) {
            //             stats.maxHp += armor.hpMod;
            //             stats.prot += armor.protMod;
            //         }
            //
            //         stats.hp = new ReactiveProperty<float>(stats.maxHp);
            //         break;
            //     case 1:
            //         foreach (var weapon in characterData.weapons) {
            //             stats.atq += weapon.atqMod;
            //             stats.crit += weapon.critMod;
            //             stats.prec += weapon.precMod;
            //         }
            //
            //         break;
            // }
            //
            // currentStats = stats;
            //currentStats = characterData.baseStats;
            //currentStats.Init();

            currentStats.atq = characterData.baseStats.atq + equipment[0].atqMod + equipment[1].atqMod;
            currentStats.prec = characterData.baseStats.prec * (equipment[0].precMod + equipment[1].precMod + 1);
            currentStats.crit = characterData.baseStats.crit * (equipment[0].critMod + equipment[1].critMod + 1);
            currentStats.maxHp = characterData.baseStats.maxHp + equipment[0].endMod + equipment[1].endMod;
            currentStats.prot = characterData.baseStats.prot * (equipment[0].protMod + equipment[1].protMod + 1);
        }

        public async Task Heal(float pts) {
            var newHealth = currentStats.hp.Value + pts;
            if (newHealth < currentStats.maxHp)
                currentStats.hp.Value = newHealth;

            await Utils.AwaitObservable(Observable.Timer(TimeSpan.FromSeconds(1)));
        }

        private float CalculateDamage(float dmg) {
            var curEnd = currentStats.hp.Value;
            curEnd -= dmg * (1 - currentStats.prot / 100f);
            if (curEnd < 0) {
                curEnd = 0;
            }

            return curEnd;
        }

        public async Task TakeDamage(float dmg) {
            currentStats.hp.Value = CalculateDamage(dmg);
            if (currentStats.hp.Value > 0) return;
            
            //TODO: spawn floating text for damage taken
            //TODO: Change this to wait for animation take damage finish
            await Utils.AwaitObservable(Observable.Timer(TimeSpan.FromSeconds(1)));
            _combatManager.Remove(this);
        }

        #endregion


        public async Task IncrementSkillWheel() {
            _skillIndex = (_skillIndex + 1) % skillWheel.Length;
            _skillIndexChanged = true;
            
            //TODO: wait for skill wheel animation finish
        }

        public async Task DecrementSkillWheel() {
            _skillIndex--;
            if (_skillIndex == -1) {
                _skillIndex = skillWheel.Length - 1;
            }
            _skillIndexChanged = true;
            
            //TODO: wait for skill wheel animation finish
        }

        public bool HasEffect(EffectType effect) => effectTypes.Value.HasFlag(effect);

        #region Unity

        private void Awake() {
            statusEffects = new ReactiveCollection<StatusEffect>();
            effectTypes = new ReactiveProperty<EffectType>(EffectType.None);
        }

        protected void Start() {
            _combatManager = CombatManager.Instance;

            _animator = GetComponentInChildren<Animator>();
            AnimatorOverrideController myOverrideController = Resources.Load<AnimatorOverrideController>(characterData.animatorController);
            _animator.runtimeAnimatorController = myOverrideController;
        }

        #endregion
    }
}