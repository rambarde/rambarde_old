﻿using System.Collections;
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
        public int nFights;
        public List<Characters.CharacterData> monsterData; 
        public FightManager fightManager;

        private string _pitch;
        public string Pitch { get { return _pitch; } set { _pitch = value; } }
        private bool _isUpgradable;
        public bool IsUpgradable { get { return _isUpgradable; } set { _isUpgradable = value; } }

        private int _gold;
        public int Gold { get { return _gold; } set { _gold = value; } }

        private List<int> _fightMax;
        public List<int> FightMax { get { return _fightMax; } set { _fightMax = value; } }

        public void Init()
        {
            _isUpgradable = true;
            switch (type)
            {
                case QuestType.Forest:
                    Gold = 400;
                    break;
                case QuestType.Crypt:
                    Gold = 700;
                    break;
            }

            if (FightMax.Count == 0)
            {
                for (int i = 0; i < 3; i++)
                    FightMax.Add(nFights);
            }
            else
            {
                for (int i = 0; i < 3; i++)
                    FightMax[i] = nFights;
            }

            //Fights init
            fightManager = new FightManager(type, !IsUpgradable, nFights); //remplacer par nFights plus tard
            fightManager.Init();
            fightManager.GenerateFights();
            fightManager.InitMonsters(monsterData);
            //Debug.Log("Wait...");
        }
    }
}

