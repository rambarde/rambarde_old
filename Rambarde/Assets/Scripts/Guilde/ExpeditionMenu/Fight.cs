using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExpeditionMenu {
    public class Fight
    {
        private List<int> _fightEncounter;
        public List<int> FightEncounter { get { return _fightEncounter; } set { _fightEncounter = value; } }

        public List<Monster> monsters;

        public Fight() { }

        public Fight(List<int> encounter)
        {
            FightEncounter = encounter;
            monsters = new List<Monster>();
        }

        public void InitMonster(List<Characters.CharacterData> monsterData)
        {
            foreach (int n in FightEncounter)
            {
                Monster monster = new Monster(monsterData[n], "MonsterDebug");
                monsters.Add(monster);
            }
        }
    }
}
