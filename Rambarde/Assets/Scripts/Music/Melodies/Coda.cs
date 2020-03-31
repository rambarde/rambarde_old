using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "Coda", menuName = "Melody/Coda")]
    class Coda : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            await StatusEffect.ApplyBuff(t, BuffType.Attack, 3);
            await StatusEffect.ApplyEffect(target, EffectType.Merciless, 1);
            await StatusEffect.ApplyEffect(target, EffectType.Rushing, 1);
            await StatusEffect.ApplyEffect(target, EffectType.Disciplined, 1);
        }
    }
}