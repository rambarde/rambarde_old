using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Characters;
using UniRx;
using UnityEngine;
using UnityEngine.UIElements;

namespace Status {
    class DeafEffect : StatusEffect {

        public DeafEffect(CharacterControl target, int turns) : base(target, turns) {
            type = EffectType.Deaf;
            spriteName = "vfx-deaf";
        }
    }
}