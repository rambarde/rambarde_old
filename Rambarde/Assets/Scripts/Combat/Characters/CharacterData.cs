using Skills;
using UniRx;
using UnityEngine;

public enum CharacterClass
{
    Stratege, Temeraire, Mage
};


namespace Characters {
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Character/CharacterData")]
    public class CharacterData : ScriptableObject {
        //public string clientName;
        public string modelName;
        public string animatorController;

        public Stats baseStats;

        public CharacterClass charType;

        [SerializeField] public Equipment[] baseEquipment;
        public ReactiveCollection<Equipment> equipments;

        public Skill[] skills;

        public Sprite clientImage;

        public void Init() {
            equipments = new ReactiveCollection<Equipment>(baseEquipment);
        }
    }
}