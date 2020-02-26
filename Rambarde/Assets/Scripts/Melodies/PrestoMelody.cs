using System.Threading.Tasks;
using Characters;
using UnityEngine;

namespace Melodies {
    [CreateAssetMenu(fileName = "PrestoMelody", menuName = "Melody/Presto")]
    public class PrestoMelody : Melody {
        public override async Task Execute(CharacterControl target) {
            await target.IncrementSkillWheel();
        }
    }
}