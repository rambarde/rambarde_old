using System.Threading.Tasks;
using Characters;
using Melodies;
using Status;
using UnityEngine;

namespace Music.Melodies {
    [CreateAssetMenu(fileName = "Cacophonie", menuName = "Melody/Cacophonie")]
    class Cacophonie : Melody {
        protected override async Task ExecuteOnTarget(CharacterControl t) {
            await t.ShuffleSkillsSlot();
        }
    }
}