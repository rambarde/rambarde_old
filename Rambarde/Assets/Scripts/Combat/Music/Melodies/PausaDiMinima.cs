using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "PausaDiMinima", menuName = "Melody/PausaDiMinima")]
    class PausaDiMinima : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            await t.Heal(25);
        }
    }
}