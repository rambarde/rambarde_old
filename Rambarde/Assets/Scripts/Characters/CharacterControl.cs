using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Skills;
using Status;
using UI;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Random = System.Random;

namespace Characters {
    public enum Team {
        PlayerTeam = 0,
        EmemyTeam = 1
    }

    public class CharacterControl : MonoBehaviour {
        public Team team;
        public Stats currentStats;
        public CharacterData characterData;
        public ReactiveCollection<StatusEffect> statusEffects;
        public ReactiveProperty<EffectType> effectTypes;
        
        public List<Skill> skillSlot;
        public Subject<SlotAction> slotAction;
        
        private static Random rng = new Random();
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
            var skill = skillSlot[_skillIndex];
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
                await IncrementSkillsSlot();
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
            data.armors.ObserveCountChanged().Subscribe(_ => UpdateStats(0)).AddTo(this);
            data.weapons.ObserveCountChanged().Subscribe(_ => UpdateStats(1)).AddTo(this);
            skillSlot = data.skills;

            UpdateStats(0);
            GetComponentInChildren<CharacterVfx>().Init();
            
            statusEffects = new ReactiveCollection<StatusEffect>();
            effectTypes = new ReactiveProperty<EffectType>(EffectType.None);
            _combatManager = CombatManager.Instance;
            _animator = GetComponentInChildren<Animator>();
            AnimatorOverrideController myOverrideController = Resources.Load<AnimatorOverrideController>(characterData.animatorController);
            _animator.runtimeAnimatorController = myOverrideController;
            slotAction = new Subject<SlotAction>();
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


        public async Task IncrementSkillsSlot() {
            _skillIndex = (_skillIndex + 1) % skillSlot.Count;
            _skillIndexChanged = true;
            
            //wait for skill wheel animation finish
            slotAction.OnNext(new SlotAction { Action = SlotAction.ActionType.Increment});
            await Utils.AwaitObservable(slotAction
                .SkipWhile(action => action.Action != SlotAction.ActionType.Sync)
                .Take(1));
        }

        public async Task DecrementSkillsSlot()
        {
            _skillIndex = --_skillIndex % skillSlot.Count;
            if (_skillIndex < 0) _skillIndex = skillSlot.Count;
            _skillIndexChanged = true;
            
            //wait for skill wheel animation finish
            slotAction.OnNext(new SlotAction { Action = SlotAction.ActionType.Decrement});
            await Utils.AwaitObservable(slotAction
                .SkipWhile(action => action.Action != SlotAction.ActionType.Sync)
                .Take(1));
        }

        public void ShuffleList<T>(IList<T> list)
        {  
            int n = list.Count;  
            while (n > 1) {  
                n--;  
                int k = rng.Next(n + 1);  
                T value = list[k];  
                list[k] = list[n];  
                list[n] = value;  
            }  
        }

        public async Task ShuffleSkillsSlot()
        {
            ShuffleList(skillSlot);
            _skillIndexChanged = true;
            
            //wait for skill wheel animation finish
            slotAction.OnNext(
                new SlotAction
                {
                    Action = SlotAction.ActionType.Shuffle,
                    Skills = skillSlot
                });
            await Utils.AwaitObservable(slotAction
                .SkipWhile(action => action.Action != SlotAction.ActionType.Sync)
                .Take(1));
        }

        public bool HasEffect(EffectType effect) => effectTypes.Value.HasFlag(effect);

        #region Unity

        private void Awake() {
            
        }

        protected void Start() {
            
        }

        #endregion
    }
}