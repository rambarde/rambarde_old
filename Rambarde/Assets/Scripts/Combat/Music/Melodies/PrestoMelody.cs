using System.Threading.Tasks;
using Characters;
using UnityEngine;

namespace Melodies {
     [CreateAssetMenu(fileName = "PrestoMelody", menuName = "Melody/Presto")]
     public class PrestoMelody : Melody {
         public override async Task Execute() {
             if (target == null) {
                 Debug.Log("Tried to execute a " + targetMode + " melody with no target");
             }
             await target.IncrementSkillWheel();
         }
     }
 }