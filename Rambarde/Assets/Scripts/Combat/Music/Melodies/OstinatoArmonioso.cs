using System.Threading.Tasks;
using Characters;
using UnityEngine;

namespace Melodies {
    [CreateAssetMenu(fileName = "OstinatoArmonioso", menuName = "Melody/OstinatoArmonioso")]
    public class OstinatoArmonioso : Melody {
        protected override async Task ExecuteOnTarget(CharacterControl t) {
            if (target == null) {
                Debug.Log("Tried to execute a " + targetMode + " melody with no target");
            }
            await target.DecrementSkillsSlot();
        }
    }
}