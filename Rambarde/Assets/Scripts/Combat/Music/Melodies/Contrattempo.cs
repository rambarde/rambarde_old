using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "Contrattempo", menuName = "Melody/Contrattempo")]
    class Contrattempo : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            Debug.Log(t.name + " ciblera: Nouvelle cible");
        }
    }
}