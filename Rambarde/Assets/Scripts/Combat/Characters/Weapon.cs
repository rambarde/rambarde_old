using UnityEngine;

namespace Characters {
    [CreateAssetMenu(fileName = "Weapon", menuName = "Character/Accessories/Weapon")]
    public class Weapon : ScriptableObject {
        public float atqMod;
        public float precMod;
        public float critMod;
    }
}