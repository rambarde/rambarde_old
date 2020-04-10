using System.Collections;
using System.Collections.Generic;
using Combat.Characters;
using UnityEngine;

namespace ExpeditionMenu
{
    public class Monster : CharacterBase
    {
        public Monster() { }

        public Monster(Characters.CharacterData data, string monsterName)
        {
            Character = data;
            _name = monsterName;

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
