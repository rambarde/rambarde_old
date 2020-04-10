using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "Prestissimo", menuName = "Melody/Prestissimo")]
    class Prestissimo : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            await StatusEffect.ApplyEffect(target, EffectType.Disciplined, 1);
            await StatusEffect.ApplyEffect(target, EffectType.Exalted, 1);
        }
    }
}