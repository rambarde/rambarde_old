using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExpeditionMenu
{
    public class Monster
    {
        [SerializeField]
        private Characters.CharacterData _character;
        public Characters.CharacterData Character { get { return _character; } set { _character = value; } }
        [SerializeField]
        private int[] _skillWheel;
        public int[] SkillWheel { get { return _skillWheel; } set { _skillWheel = value; } }
        [SerializeField]
        private string _monsterName;
        public string MonsterName { get { return _monsterName; } set { _monsterName = value; } }

        public Monster() { }

        public Monster(Characters.CharacterData data, string monsterName)
        {
            Character = data;
            MonsterName = monsterName;

            SkillWheel = GenerateSkillWheel();
        }

        private int[] GenerateSkillWheel()
        {
            int[] skillWheel = new int[4];
            List<int> skillChoice = new List<int>();

            for (int i = 0; i < Character.skills.Length; i++)
                skillChoice.Add(i);

            for(int i = 0; i < skillWheel.Length; i++)
            {
                var n = Random.Range(0, skillChoice.Count);
                skillWheel[i] = skillChoice[n];
                skillChoice.RemoveAt(n);
            }

            return skillWheel;
        }
    }
}
