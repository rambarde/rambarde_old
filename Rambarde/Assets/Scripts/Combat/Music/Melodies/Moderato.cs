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
            await StatusEffect.ApplyBuff(t, -2, BuffType.Attack);
            await StatusEffect.ApplyBuff(t, 3, BuffType.Protection);
        }
    }
}