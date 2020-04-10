using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "Kyrielle", menuName = "Melody/Kyrielle")]
    class Kyrielle : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            Debug.Log("Les roues de tout le monde n'avancent pas à la fin du tour");
        }
    }
}