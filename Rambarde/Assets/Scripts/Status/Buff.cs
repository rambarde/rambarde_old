using System;
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
        private float _modifier;
        public readonly BuffType buffType;
        public int level;

        public Buff(CharacterControl target, int level, BuffType buffType) : base(target, Math.Abs(level)) {
            this.buffType = buffType;
            this.level = level;
            
            ComputeModifier();
        }

        private void ComputeModifier() {
            switch (level) {
                case 1:
                    _modifier = 1.3f;
                    break;
                case 2:
                    _modifier = 1.4f;
                    break;
                case 3:
                    _modifier = 1.5f;
                    break;
                case -1:
                    _modifier = 0.75f;
                    break;
                case -2:
                    _modifier = 0.65f;
                    break;
                case -3:
                    _modifier = 0.6f;
                    break;
                default:
                    throw new InvalidEnumArgumentException($"{level} is not a valid buff modifier.");
            }
        }

        protected override Task PostTurnEnd() {
            // fade out level
            Remove();
            level -= Math.Sign(level);
            ComputeModifier();
            Apply();
            
            return base.PostTurnEnd();
        }

        protected override Task Apply() {
            switch (buffType) {
                case BuffType.Attack :
                    target.currentStats.atq *= _modifier;
                    break;
                case BuffType.Protection :
                    target.currentStats.prot *= _modifier;
                    break;
                case BuffType.Critical :
                    target.currentStats.crit *= _modifier;
                    break;
                default:
                    Debug.LogError("error : tried to apply an unknown buff type [" + buffType +"]");
                    break;
            }

            return base.Apply();
        }

        protected override Task Remove() {
            switch (buffType) {
                case BuffType.Attack :
                    target.currentStats.atq /= _modifier;
                    break;
                case BuffType.Protection :
                    target.currentStats.prot /= _modifier;
                    break;
                case BuffType.Critical :
                    target.currentStats.crit /= _modifier;
                    break;
                default:
                    Debug.LogError("error : tried to remove an unknown buff type [" + buffType +"]");
                    break;
            }
            
            return base.Remove();
        }
    }
}