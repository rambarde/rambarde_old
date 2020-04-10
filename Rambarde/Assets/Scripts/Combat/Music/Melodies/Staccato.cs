using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "Staccato", menuName = "Melody/Staccato")]
    class Staccato : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            await StatusEffect.ApplyEffect(target, EffectType.HealthRegen, 3);
        }
    }
}