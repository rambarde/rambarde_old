using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "Fuocoso", menuName = "Melody/Fuocoso")]
    class Fuocoso : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            await StatusEffect.ApplyBuff(t, BuffType.Attack, 1);
            await StatusEffect.ApplyBuff(t, BuffType.Critical, 1);
        }
    }
}