using Skills;
using UniRx;
using UnityEngine;

namespace Characters {
    [CreateAssetMenu(fileName = "CharacterData", menuName = "Character/CharacterData")]
    public class CharacterData : ScriptableObject {
        public string modelName;
        public string animatorController;

        public Stats baseStats;

        [SerializeField] private Weapon[] baseWeapons;
        [SerializeField] private Armor[] baseArmors;
        public ReactiveCollection<Weapon> weapons;
        public ReactiveCollection<Armor> armors;

        public Skill[] skills;

        public void Init() {
            weapons = new ReactiveCollection<Weapon>(baseWeapons);
            armors = new ReactiveCollection<Armor>(baseArmors);
        }
    }
}