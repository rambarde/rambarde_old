using UnityEngine;

public enum EquipType
{
    WEAPON, ARMOR
};

namespace Characters {
    [CreateAssetMenu(fileName = "Equipment", menuName = "Character/Accessories/Equipment")]
    public class Equipment : ScriptableObject {
        public string equipmentName;
        public Sprite sprite;

        public EquipType type;
        public CharacterClass allowedType;

        public float atqMod;
        public float precMod;
        public float critMod;
        public float protMod;
        public float endMod;

        public int price;
        public int numberOwned = 0;
        public int numberEquiped = 0;
    }
}