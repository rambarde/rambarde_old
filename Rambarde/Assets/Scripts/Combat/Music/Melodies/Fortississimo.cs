using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "Fortississimo", menuName = "Melody/Fortississimo")]
    class Fortississimo : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            await StatusEffect.ApplyBuff(t, BuffType.Attack, 2);
        }
    }
}