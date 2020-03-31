using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "Decrescendo", menuName = "Melody/Decrescendo")]
    class Decrescendo : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            await StatusEffect.ApplyBuff(t, BuffType.Protection, -2);
        }
    }
}