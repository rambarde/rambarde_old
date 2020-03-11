using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "Moderato", menuName = "Melody/Moderato")]
    class Moderato : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            await StatusEffect.ApplyBuff(t, BuffType.Attack, -2);
            await StatusEffect.ApplyBuff(t, BuffType.Protection, 3);
        }
    }
}