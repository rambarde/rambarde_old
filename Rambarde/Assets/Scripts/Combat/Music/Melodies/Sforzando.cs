using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "Sforzando", menuName = "Melody/Sforzando")]
    class Sforzando : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            await StatusEffect.ApplyEffect(target, EffectType.Destabilized, 2);
        }
    }
}