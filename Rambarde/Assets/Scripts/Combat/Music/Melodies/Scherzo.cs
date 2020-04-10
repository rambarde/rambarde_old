using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies
{
    [CreateAssetMenu(fileName = "Scherzo", menuName = "Melody/Scherzo")]
    class Scherzo : Melody
    {
        protected override async Task ExecuteOnTarget(CharacterControl t)
        {
            Debug.Log(t.name + " se ciblera lui-même pour la prochaine compétence");
        }
    }
}