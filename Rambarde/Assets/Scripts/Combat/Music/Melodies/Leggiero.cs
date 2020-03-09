using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "Leggiero", menuName = "Melody/Leggiero")]
    class Leggiero : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            Debug.Log("Annule un niveau de changements de stats de: " + t.name);
        }
    }
}