using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "PausaDiBreve", menuName = "Melody/PausaDiBreve")]
    class PausaDiBreve : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            await t.Heal(80);
        }
    }
}