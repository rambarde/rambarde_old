using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "Jam", menuName = "Melody/Jam")]
    class Jam : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            Debug.Log("Roue de compétences de " + t.name + "réarrangée");
        }
    }
}