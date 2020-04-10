using Characters;
using UnityEngine;

namespace Combat.Characters
{
    public class CharacterBase
    {
        [SerializeField]
        private CharacterData _character;
        public CharacterData Character { get { return _character; } set { _character = value; } }
        [SerializeField]
        private int[] _skillWheel;
        public int[] SkillWheel { get { return _skillWheel; } set { _skillWheel = value; } }
        [SerializeField] protected string _name;
        public string Name { get { return _name; } set { _name = value; } }

    }
}