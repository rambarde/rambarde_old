using System;
using System.Linq;
using System.Threading.Tasks;
using Characters;
using TMPro;
using UniRx;
using UnityEngine;

namespace Status {
    [Serializable]
    public abstract class StatusEffect {
        public ReactiveProperty<int> turnsLeft;
        public string spriteName;
        public EffectType type;
        protected CharacterControl Target;

        private bool _justApplied = true;

        // Abstract methods
        protected virtual async Task PreTurnStart() { }

        protected virtual async Task PostTurnEnd() { }
        
        protected virtual async Task Apply() { }
        protected virtual async Task Remove() { }

        // Final methods
        public async Task TurnStart() {
            await PreTurnStart();

            if (--turnsLeft.Value == 0) {
                RemoveEffect();
            }
        }

        public async Task TurnEnd() {
            if (_justApplied) {
                _justApplied = false;
                return;
            }

            await PostTurnEnd();
        }

        public void RemoveEffect() {
            Target.statusEffects.Remove(this);
            Target.effectTypes.Value &= ~type;
        }

        public static async Task ApplyEffect<T>(CharacterControl target, Lazy<T> addedEffect, int addedTurns = 0) where T : StatusEffect {
            // TODO: applying effect animation

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
        
        public static async Task ApplyEffect(CharacterControl target, EffectType effectType, int nbrTurn, float value = 0.0f) {
            // TODO: applying effect animation

            var effects = target.statusEffects;
            StatusEffect effect = effects.FirstOrDefault(e => e.type == effectType);;
            if (effect is null) {
                effect = CreateEffect(target, effectType, nbrTurn, value);
                effects.Add(effect);
                target.effectTypes.Value |= effectType;
            } else {
                effect.AddTurns(nbrTurn);
            }

        }

        public static StatusEffect CreateEffect(CharacterControl target, EffectType effectType, int nbrTurn, float value = 0.0f) {
            switch (effectType) {
                case EffectType.Poison :
                    return new PoisonEffect(target, value, nbrTurn);
                case EffectType.HealthRegen :
                    return new HealthRegen(target, value, nbrTurn);
                case EffectType.Deaf :
                    return new DeafEffect(target, nbrTurn);
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
    public enum EffectType : int{
        None = 0,
        Poison = 1,
        HealthRegen = 2,
        Deaf = 4,
        
    }
}