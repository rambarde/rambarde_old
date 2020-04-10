using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "Accelerando", menuName = "Melody/Accelerando")]
    class Accelerando : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            await t.IncrementSkillsSlot();
        }
    }
}