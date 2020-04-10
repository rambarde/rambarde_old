using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "MaestosoDaCapo", menuName = "Melody/MaestosoDaCapo")]
    class MaestosoDaCapo : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            await StatusEffect.ApplyEffect(target, EffectType.Grace, 5);
        }
    }
}