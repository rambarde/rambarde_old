using UnityEngine;

namespace Characters {
    [CreateAssetMenu(fileName = "Armor", menuName = "Character/Accessories/Armor")]
    public class Armor : ScriptableObject {
        public float protMod;
        public float hpMod;
    }
}