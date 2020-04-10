using System.ComponentModel;
using System.Threading.Tasks;
using Characters;
using UnityEngine;

namespace Status {
    public enum BuffType {
        None,
        Attack,
        Critical,
        Protection,
    }
    
    public class Buff : StatusEffect {
        private readonly float _modifiedValue;
        public readonly BuffType _buffType;
        public readonly int _modifier;

        public Buff(CharacterControl target, int turns, BuffType buffType, int modifier) : base(target, turns) {
            _buffType = buffType;
            _modifier = modifier;

            switch (_modifier) {
                case 1:
                    _modifiedValue = 1.3f;
                    break;
                case 2:
                    _modifiedValue = 1.4f;
                    break;
                case 3:
                    _modifiedValue = 1.5f;
                    break;
                case -1:
                    _modifiedValue = 0.75f;
                    break;
                case -2:
                    _modifiedValue = 0.65f;
                    break;
                case -3:
                    _modifiedValue = 0.6f;
                    break;
                default:
                    throw new InvalidEnumArgumentException($"{_modifier} is not a valid buff modifier.");
            }
        }

        protected override Task Apply() {
            switch (_buffType) {
                case BuffType.Attack :
                    target.currentStats.atq *= _modifiedValue;
                    break;
                case BuffType.Protection :
                    target.currentStats.prot *= _modifiedValue;
                    break;
                case BuffType.Critical :
                    target.currentStats.crit *= _modifiedValue;
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
                    target.currentStats.atq /= _modifiedValue;
                    break;
                case BuffType.Protection :
                    target.currentStats.prot /= _modifiedValue;
                    break;
                case BuffType.Critical :
                    target.currentStats.crit /= _modifiedValue;
                    break;
                default:
                    Debug.LogError("error : tried to remove an unknown buff type [" + _buffType +"]");
                    break;
            }
            
            return base.Remove();
        }
    }
}