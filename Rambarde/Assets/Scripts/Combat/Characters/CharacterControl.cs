﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Skills;
using Status;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Characters {
    public enum Team {
        PlayerTeam = 0,
        EmemyTeam = 1
    }

    public class CharacterControl : MonoBehaviour {
        public string clientName;
        public Team team;
        public int clientNumber;
        public Skill[] skillWheel;
        public Stats currentStats;
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

        public async Task ExecTurn()
        {

            if (HasEffect(EffectType.Confused))
            {
                int r = Random.Range(0, skillWheel.Length);
                for (int i = 0; i <= r; ++i)
                    await IncrementSkillWheel();
            }

            var skill = skillWheel[_skillIndex];

            if (HasEffect(EffectType.Dizzy))
                _skillIndexChanged = true;
            else
            {
                //calculate chances of miss and critical hit, and merciless effect

                // Play and wait for skillAnimation to finish
                await SkillPreHitAnimation(skill.animationName);
                // Execute the skill
                await skill.Execute(this);

                if (HasEffect(EffectType.Exalted))
                {
                    await IncrementSkillWheel();
                    await SkillPreHitAnimation(skill.animationName);
                    await skill.Execute(this);
                }
            }

            // Apply effects at the end of the turn
            for (var i = statusEffects.Count - 1; i >= 0; --i)
                await statusEffects[i].TurnEnd();

            // Increment skillIndex ONLY if effects did not affect the wheel
            if (!_skillIndexChanged)
                await IncrementSkillWheel();

            _skillIndexChanged = false;
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

        public void Init(CharacterData data)
        {
            characterData = data;
            data.Init();
            data.armors.ObserveCountChanged().Subscribe(_ => UpdateStats(0)).AddTo(this);
            data.weapons.ObserveCountChanged().Subscribe(_ => UpdateStats(1)).AddTo(this);
            skillWheel = data.skills;

            UpdateStats(0);
            GetComponentInChildren<CharacterVfx>().Init();
        }

        //public void Init(ExpeditionMenu.Monster monster)
        //{//CharacterData data) {
        //    characterData = data;
        //    data.Init();
        //    data.armors.ObserveCountChanged().Subscribe(_ => UpdateStats(0)).AddTo(this);
        //    data.weapons.ObserveCountChanged().Subscribe(_ => UpdateStats(1)).AddTo(this);
        //    skillWheel = data.skills;

        //    characterData = monster.Character;
        //    characterData.Init();
        //    characterData.armors.ObserveCountChanged().Subscribe(_ => UpdateStats(0)).AddTo(this);
        //    characterData.weapons.ObserveCountChanged().Subscribe(_ => UpdateStats(1)).AddTo(this);

        //    Skills.Skill[] temp = new Skills.Skill[4];

        //    for (int j = 0; j < monster.SkillWheel.Length; j++)
        //        temp[j] = characterData.skills[monster.SkillWheel[j]];
        //    skillWheel = temp;

        //    UpdateStats(0);
        //    GetComponentInChildren<CharacterVfx>().Init();
        //}

        //public void Init(Client client){//CharacterData data) {
        //    characterData = data;
        //    data.Init();
        //    data.armors.ObserveCountChanged().Subscribe(_ => UpdateStats(0)).AddTo(this);
        //    data.weapons.ObserveCountChanged().Subscribe(_ => UpdateStats(1)).AddTo(this);
        //    skillWheel = data.skills;

        //    characterData = client.Character;
        //    characterData.Init();
        //    characterData.armors.ObserveCountChanged().Subscribe(_ => UpdateStats(0)).AddTo(this);
        //    characterData.weapons.ObserveCountChanged().Subscribe(_ => UpdateStats(1)).AddTo(this);

        //    Skills.Skill[] temp = new Skills.Skill[4];

        //    for (int j = 0; j < client.SkillWheel.Length; j++)
        //        temp[j] = characterData.skills[client.SkillWheel[j]];
        //    skillWheel = temp;

        //    UpdateStats(0);
        //    GetComponentInChildren<CharacterVfx>().Init();
        //}

        public void Init(CharacterData data, int[] intSkillWheel)
        {
            characterData = data;
            characterData.Init();
            characterData.armors.ObserveCountChanged().Subscribe(_ => UpdateStats(0)).AddTo(this);
            characterData.weapons.ObserveCountChanged().Subscribe(_ => UpdateStats(1)).AddTo(this);

            Skill[] temp = new Skill[4];

            for (int j = 0; j < intSkillWheel.Length; j++)
                temp[j] = characterData.skills[intSkillWheel[j]];
            skillWheel = temp;

            UpdateStats(0);
            GetComponentInChildren<CharacterVfx>().Init();
        }

        private void UpdateStats(int accessory) {
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
            currentStats = characterData.baseStats;
            currentStats.Init();
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

        public void PreventSkillWheelIncrement()
        {
            _skillIndexChanged = true;
        }

        public async Task ShuffleSkillWheel()
        {
            for (int i = skillWheel.Length - 1; i <= 1; --i)
            {
                int j = Random.Range(0, i + 1);
                Skill tmp = skillWheel[i];
                skillWheel[i] = skillWheel[j];
                skillWheel[j] = tmp;
            }
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