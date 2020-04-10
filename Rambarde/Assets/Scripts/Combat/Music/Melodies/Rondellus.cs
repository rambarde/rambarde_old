using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "Rondellus", menuName = "Melody/Rondellus")]
    class Rondellus : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            Debug.Log("Toutes les roues de compétences réarrangées");
        }
    }
}