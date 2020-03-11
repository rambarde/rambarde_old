using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "Staccatissimo", menuName = "Melody/Staccatissimo")]
    class Staccatissimo : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            await StatusEffect.ApplyEffect(target, EffectType.HealthRegen, 5);
        }
    }
}