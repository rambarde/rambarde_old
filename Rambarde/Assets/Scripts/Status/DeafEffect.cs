using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Characters;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

namespace Status {
    class DeafEffect : StatusEffect {

        public DeafEffect(CharacterControl target, int turns) {
            type = EffectType.Deaf;
            Target = target;
            spriteName = "vfx-deaf";
            turnsLeft = new ReactiveProperty<int>(turns);
        }
        protected override Task PreTurnStart() {
            return base.PreTurnStart();
        }

        protected override Task PostTurnEnd() {
            return null;
        }
    }
}