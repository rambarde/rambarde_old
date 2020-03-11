using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "PausaDiCroma", menuName = "Melody/PausaDiCroma")]
    class PausaDiCroma : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            await t.Heal(30);
        }
    }
}