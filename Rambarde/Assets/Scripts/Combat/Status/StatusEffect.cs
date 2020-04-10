using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Characters;
using TMPro;
using UniRx;
using UnityEngine;

namespace Status {
    [Serializable]
    public abstract class StatusEffect {

        public static int INFINITE = -1;
        
        public ReactiveProperty<int> turnsLeft;
        public string spriteName;

        protected CharacterControl target;
        public EffectType type;

        private bool _justApplied = true;

        protected StatusEffect(CharacterControl target, int turns) {
            this.target = target;
            turnsLeft = new ReactiveProperty<int>(turns);
        }

        // Abstract methods
        protected virtual async Task PreTurnStart() { }

        protected virtual async Task PostTurnEnd() { }
        
        protected virtual async Task Apply() { }
        protected virtual async Task Remove() { }

        // Final methods
        public async Task TurnStart() {
            await PreTurnStart();

            if (turnsLeft.Value != INFINITE) {
                --turnsLeft.Value;
            }

            if (turnsLeft.Value  == 0) {
                await RemoveEffect();
            }
        }

        public async Task TurnEnd() {
            if (_justApplied) {
                _justApplied = false;
                return;
            }

            await PostTurnEnd();
        }

        public async Task RemoveEffect() {
            await Remove();
            target.statusEffects.Remove(this);
            target.effectTypes.Value &= ~type;
        }

        public static async Task ApplyEffect<T>(CharacterControl target, Lazy<T> addedEffect, int addedTurns = 0) where T : StatusEffect {
            //TODO: applying effect animation

            var effects = target.statusEffects;
            var effect = effects.FirstOrDefault(x => x.GetType() == typeof(T));

            if (effect is null) {
                effects.Add(addedEffect.Value);
                target.effectTypes.Value |= addedEffect.Value.type;
            }
            else {
                effect.AddTurns(addedTurns);
            }

        }
        
        public static async Task ApplyEffect(CharacterControl target, EffectType effectType, int nbrTurn) {
            // TODO: applying effect animation

            var effects = target.statusEffects;
            StatusEffect effect = effects.FirstOrDefault(e => e.type == effectType);
            if (effect is null) {
                effect = CreateEffect(target, effectType, nbrTurn);
                effects.Add(effect);
                target.effectTypes.Value |= effectType;
            } else {
                effect.turnsLeft.Value = Math.Max(nbrTurn, effect.turnsLeft.Value);
            }
        }
        
        public static async Task ApplyBuff(CharacterControl target, BuffType buffType, int nbrTurn) {
            // TODO: applying effect animation

            var effects = target.statusEffects;
            Buff effect = (Buff) effects.FirstOrDefault(e => 
                e.type == EffectType.Buff
                && ((Buff) e)._buffType == buffType);
            
            if (effect is null) {
                effect = new Buff(target, nbrTurn, buffType, nbrTurn);
                effects.Add(effect);
                target.effectTypes.Value |= EffectType.Buff;
            } else {
                effect.turnsLeft.Value = Math.Max(nbrTurn, effect.turnsLeft.Value);
            }
        }

        public static StatusEffect CreateEffect(CharacterControl target, EffectType effectType, int nbrTurn) {
            switch (effectType) {
                case EffectType.Poison :
                    return new PoisonEffect(target, 15, nbrTurn);
                case EffectType.HealthRegen :
                    return new HealthRegen(target, 15, nbrTurn);
                case EffectType.Deaf :
                    return new DeafEffect(target, nbrTurn);
                case EffectType.Destabilized :
                    return new Destabilized(target, nbrTurn);
                case EffectType.Merciless :
                    return new Merciless(target, nbrTurn);
                case EffectType.Dizzy :
                    return new Dizzy(target, nbrTurn);
                case EffectType.Rushing :
                    return new Rushing(target, nbrTurn);
                case EffectType.Lagging :
                    return new Lagging(target, nbrTurn);
                case EffectType.Confused :
                    return new Confused(target, nbrTurn);
                case EffectType.Unpredictable :
                    return new Unpredictable(target, nbrTurn);
                case EffectType.Inapt :
                    return new Inapt(target, nbrTurn);
                case EffectType.Exalted :
                    return new Exalted(target, nbrTurn);
                case EffectType.Disciplined :
                    return new Disciplined(target, nbrTurn);
                case EffectType.Cursed :
                    return new Cursed(target, nbrTurn);
                case EffectType.Condemned :
                    return new Condemned(target, nbrTurn);
                case EffectType.Invisible :
                    return new Invisible(target, nbrTurn);
                case EffectType.Marked :
                    Debug.Log("Oh hi Mark!");
                    return new Marked(target, nbrTurn);
                case EffectType.Grace :
                    return new Grace(target, nbrTurn);
                case EffectType.Counter :
                    return new Counter(target, nbrTurn);
                default:
                    Debug.LogError("Error : tried to create an invalid status effect [" + effectType + "]");
                    return null;
            }
        }
        

        public void AddTurns(int n) {
            turnsLeft.Value += n;
        }
    }

    [Flags]
    public enum EffectType {
        None = 0,
        Buff = 1 << 1,//-
        HealthRegen = 1 << 2,//-
        Poison = 1 << 3,//-
        Destabilized = 1 << 4,//-
        Merciless = 1 << 5,//
        Dizzy = 1 << 6,//-
        Rushing = 1 << 7,//
        Lagging = 1 << 8,//
        Confused = 1 << 9,//-
        Unpredictable  = 1 << 10,//
        Inapt = 1 << 11,//
        Exalted = 1 << 12,//~
        Disciplined = 1 << 13,//
        Cursed = 1 << 14,//
        Condemned = 1 << 15,//-
        Deaf = 1 << 16,//-
        Invisible = 1 << 17,//
        Marked = 1 << 18,//
        Grace = 1 << 19,//
        Counter = 1 << 20,//
    }
}