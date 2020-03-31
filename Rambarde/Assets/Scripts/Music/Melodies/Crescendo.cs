using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "Crescendo", menuName = "Melody/Crescendo")]
    class Crescendo : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            await StatusEffect.ApplyBuff(t, BuffType.Attack, 1);
            await StatusEffect.ApplyBuff(t, BuffType.Critical, 2);
        }
    }
}