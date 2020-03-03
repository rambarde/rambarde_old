using System;
using System.Threading.Tasks;
using Characters;
using Status;
using UnityEngine;

namespace Melodies {
    [CreateAssetMenu(fileName = "OstinatoSaturato", menuName = "Melody/OstinatoSaturato")]
    class OstinatoSaturato : Melody {
        protected override async Task ExecuteOnTarget(CharacterControl t) {
            if (t is null) {
                return;
            }

            await StatusEffect.ApplyEffect(target, 
                new Lazy<DeafEffect>(() => new DeafEffect(t, 2)));
            await t.DecrementSkillWheel();
        }
    }
}