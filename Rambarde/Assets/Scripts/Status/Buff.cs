using System.ComponentModel;
using System.Threading.Tasks;
using Characters;
using UnityEngine;

namespace Status {
    public enum BuffType {
        Attack,
        Critical,
        Protection,
    }
    
    public abstract class StatModifier : StatusEffect {
        private readonly float _modifiedValue;
        private readonly BuffType _buffType;
        private readonly int _modifier;

        public StatModifier(CharacterControl target, int turns, BuffType buffType, int modifier) : base(target, turns) {
            _buffType = buffType;
            _modifier = modifier;
            
            _modifiedValue = _modifier switch {
                1  => 1.3f,
                2  => 1.4f,
                3  => 1.5f,
                -1 => 0.75f,
                -2 => 0.65f,
                -3 => 0.6f,
                _ => throw new InvalidEnumArgumentException($"{_modifier} is not a valid buff modifier.") 
            };

        }

        protected override Task Apply() {
            switch (_buffType) {
                case BuffType.Attack :
                    Target.currentStats.atq *= _modifiedValue;
                    break;
                case BuffType.Protection :
                    Target.currentStats.prot *= _modifiedValue;
                    break;
                case BuffType.Critical :
                    Target.currentStats.crit *= _modifiedValue;
                    break;
                default:
                    Debug.LogError("error : tried to apply an unknown buff type [" + _buffType +"]");
                    break;
            }

            return base.Apply();
        }

        protected override Task Remove() {
            switch (_buffType) {
                case BuffType.Attack :
                    Target.currentStats.atq /= _modifiedValue;
                    break;
                case BuffType.Protection :
                    Target.currentStats.prot /= _modifiedValue;
                    break;
                case BuffType.Critical :
                    Target.currentStats.crit /= _modifiedValue;
                    break;
                default:
                    Debug.LogError("error : tried to remove an unknown buff type [" + _buffType +"]");
                    break;
            }
            
            return base.Remove();
        }
    }
}