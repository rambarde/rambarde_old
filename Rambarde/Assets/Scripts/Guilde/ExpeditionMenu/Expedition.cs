using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace ExpeditionMenu
{
    public enum QuestType
    {
        Forest = 0,
        Crypt = 1
    }

    [CreateAssetMenu(fileName = "Expedition", menuName = "Expedition")]
    public class Expedition : ScriptableObject
    {
        public string expeName;
        public QuestType type;
        public Sprite map;
        public Sprite upgradedMap;
        public List<Characters.CharacterData> monsterData; 
        public FightManager fightManager;

        private string _pitch;
        public string Pitch { get { return _pitch; } set { _pitch = value; } }
        private bool _isUpgradable;
        public bool IsUpgradable { get { return _isUpgradable; } set { _isUpgradable = value; } }

        public void Init()
        {
            _isUpgradable = true;
            fightManager = new FightManager(type, !IsUpgradable, 2);
            fightManager.Init();
            fightManager.GenerateFights();
            fightManager.InitMonsters(monsterData);
            //Debug.Log("Wait...");
        }
    }
}

