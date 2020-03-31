using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "Fortissimo", menuName = "Melody/Fortissimo")]
    class Fortissimo : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            await StatusEffect.ApplyBuff(t, BuffType.Attack, 3);
            await StatusEffect.ApplyBuff(t, BuffType.Critical, 2);
        }
    }
}