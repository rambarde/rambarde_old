using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "Pacato", menuName = "Melody/Pacato")]
    class Pacato : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            Debug.Log("Guérit tous les status des alliés");
        }
    }
}