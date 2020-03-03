using System.Threading.Tasks;
using UnityEngine;

namespace Melodies {
    [CreateAssetMenu(fileName = "OstinatoArmonioso", menuName = "Melody/OstinatoArmonioso")]
    public class OstinatoArmonioso : Melody {
        public override async Task Execute() {
            if (target == null) {
                Debug.Log("Tried to execute a " + targetMode + " melody with no target");
            }
            await target.DecrementSkillWheel();
        }
    }
}