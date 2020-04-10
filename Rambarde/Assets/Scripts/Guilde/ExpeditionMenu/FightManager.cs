using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ExpeditionMenu
{
    public enum FightDifficulty
    {
        Easy = 0,
        Normal = 1,
        Hard = 2,
        Boss = 3
    }

    public class FightManager
    {
        public QuestType type;
        public bool upgraded;     //false = level 1; true = level 2
        public int nFights;
        public List<Fight> fights = new List<Fight>();
        string test = "";

        private List<Fight> easyFights = new List<Fight>();
        private List<Fight> normalFights = new List<Fight>();
        private List<Fight> hardFights = new List<Fight>();
        private List<Fight> bossFights = new List<Fight>();

        public FightManager() { }

        public FightManager(QuestType t, bool b, int n)
        {
            type = t;
            upgraded = b;
            nFights = n;
        }

        public void Init()
        {
            switch (type)
            {
                case QuestType.Forest:
                    #region defEncounterForest
                    //0=goblin,1=commandant goblin,2=orc gourdin,3=orc archer,4=commandant orc,5=golem de pierre,6 = treant
                    //easy encounters
                    easyFights.Add(new Fight(new List<int>() { 0, 0, 0 }));
                    easyFights.Add(new Fight(new List<int>() { 0, 1 }));
                    easyFights.Add(new Fight(new List<int>() { 2, 2 }));
                    easyFights.Add(new Fight(new List<int>() { 3, 3 }));
                    easyFights.Add(new Fight(new List<int>() { 2, 3 }));
                    easyFights.Add(new Fight(new List<int>() { 5, 2 }));
                    easyFights.Add(new Fight(new List<int>() { 5, 3 }));
                    easyFights.Add(new Fight(new List<int>() { 5, 5 }));

                    //normal encounters
                    normalFights.Add(new Fight(new List<int>() { 1, 0, 0 }));
                    normalFights.Add(new Fight(new List<int>() { 2, 2, 2 }));
                    normalFights.Add(new Fight(new List<int>() { 3, 3, 3 }));
                    normalFights.Add(new Fight(new List<int>() { 2, 2, 6 }));
                    normalFights.Add(new Fight(new List<int>() { 1, 0, 2 }));
                    normalFights.Add(new Fight(new List<int>() { 1, 0, 5 }));
                    normalFights.Add(new Fight(new List<int>() { 5, 2, 2 }));
                    normalFights.Add(new Fight(new List<int>() { 5, 5, 3 }));
                    normalFights.Add(new Fight(new List<int>() { 6, 6 }));
                    normalFights.Add(new Fight(new List<int>() { 6, 2, 0 }));

                    //hard encounters
                    hardFights.Add(new Fight(new List<int>() { 1, 2, 2 }));
                    hardFights.Add(new Fight(new List<int>() { 1, 3, 3 }));
                    hardFights.Add(new Fight(new List<int>() { 1, 2, 3 }));
                    hardFights.Add(new Fight(new List<int>() { 5, 5, 5 }));
                    hardFights.Add(new Fight(new List<int>() { 5, 5, 6 }));
                    hardFights.Add(new Fight(new List<int>() { 6, 6, 3 }));
                    hardFights.Add(new Fight(new List<int>() { 1, 5, 2 }));
                    hardFights.Add(new Fight(new List<int>() { 6, 5, 2 }));

                    //boss encounters
                    bossFights.Add(new Fight(new List<int>() { 4, 2, 2 }));
                    bossFights.Add(new Fight(new List<int>() { 4, 3, 3 }));
                    bossFights.Add(new Fight(new List<int>() { 4, 2, 3 }));
                    bossFights.Add(new Fight(new List<int>() { 4, 1, 0 }));
                    bossFights.Add(new Fight(new List<int>() { 6, 6, 6 }));
                    bossFights.Add(new Fight(new List<int>() { 6, 6, 1 }));
                    #endregion
                    break;

                case QuestType.Crypt:
                    #region defEncounterCrypt
                    //0=skeleton,1=annoyed skeleton,2=skeleton w/ a cold,3=ghost,4=lost soul,5=wisp,6=blaze
                    //easy encounters
                    easyFights.Add(new Fight(new List<int>() { 0, 0, 0 }));
                    easyFights.Add(new Fight(new List<int>() { 2, 2 }));
                    easyFights.Add(new Fight(new List<int>() { 4, 4 }));
                    easyFights.Add(new Fight(new List<int>() { 5, 5 }));
                    easyFights.Add(new Fight(new List<int>() { 4, 0, 0 }));
                    easyFights.Add(new Fight(new List<int>() { 0, 0, 2 }));

                    //normal encounters
                    normalFights.Add(new Fight(new List<int>() { 1, 1 }));
                    normalFights.Add(new Fight(new List<int>() { 1, 0, 0 }));
                    normalFights.Add(new Fight(new List<int>() { 4, 4, 4 }));
                    normalFights.Add(new Fight(new List<int>() { 4, 5, 5 }));
                    normalFights.Add(new Fight(new List<int>() { 3, 5, 0 }));
                    normalFights.Add(new Fight(new List<int>() { 3, 4, 4 }));
                    normalFights.Add(new Fight(new List<int>() { 3, 2, 0 }));
                    normalFights.Add(new Fight(new List<int>() { 3, 5, 4 }));
                    normalFights.Add(new Fight(new List<int>() { 1, 0, 5 }));
                    normalFights.Add(new Fight(new List<int>() { 1, 2, 0 }));

                    //hard encounters
                    hardFights.Add(new Fight(new List<int>() { 1, 4, 2 }));
                    hardFights.Add(new Fight(new List<int>() { 1, 3, 5 }));
                    hardFights.Add(new Fight(new List<int>() { 1, 1, 3 }));
                    hardFights.Add(new Fight(new List<int>() { 1, 1, 1 }));
                    hardFights.Add(new Fight(new List<int>() { 2, 2, 2 }));
                    hardFights.Add(new Fight(new List<int>() { 5, 5, 5 }));

                    //boss encounters
                    bossFights.Add(new Fight(new List<int>() { 6, 5, 5 }));
                    bossFights.Add(new Fight(new List<int>() { 6, 1, 1 }));
                    bossFights.Add(new Fight(new List<int>() { 6, 4, 3 }));
                    #endregion
                    break;
            }
        }

        public void GenerateFights()
        {
            int diff;
            switch (type)
            {
                #region forestFights
                case QuestType.Forest:
                    if (upgraded)
                    {
                        for (int i = 0; i < nFights - 1; i++)
                        {
                            diff = Random.Range(0, 100);
                            if (diff < 40)
                                PickAFight(FightDifficulty.Easy);
                            if (diff < 80)
                                PickAFight(FightDifficulty.Normal);
                            else
                                PickAFight(FightDifficulty.Hard);
                        }

                        //last fight
                        diff = Random.Range(0, 100);
                        if (diff < 70)
                            PickAFight(FightDifficulty.Hard);
                        else
                            PickAFight(FightDifficulty.Boss);
                    }
                    else
                    {
                        for (int i = 0; i < nFights - 1; i++)
                        {
                            diff = Random.Range(0, 100);
                            if (diff < 10)
                                PickAFight(FightDifficulty.Easy);
                            if (diff < 60)
                                PickAFight(FightDifficulty.Normal);
                            else
                                PickAFight(FightDifficulty.Hard);
                        }

                        //last fight
                        diff = Random.Range(0, 100);
                        if (diff < 30)
                            PickAFight(FightDifficulty.Hard);
                        else
                            PickAFight(FightDifficulty.Boss);
                    }
                    break;
                #endregion
                #region cryptFights
                case QuestType.Crypt:
                    if (upgraded)
                    {
                        for (int i = 0; i < nFights - 1; i++)
                        {
                            diff = Random.Range(0, 100);
                            if (diff < 40)
                                PickAFight(FightDifficulty.Easy);
                            if (diff < 80)
                                PickAFight(FightDifficulty.Normal);
                            else
                                PickAFight(FightDifficulty.Hard);
                        }
                        PickAFight(FightDifficulty.Hard);
                    }
                    else
                    {
                        for (int i = 0; i < nFights - 1; i++)
                        {
                            diff = Random.Range(0, 100);
                            if (diff < 10)
                                PickAFight(FightDifficulty.Easy);
                            if (diff < 60)
                                PickAFight(FightDifficulty.Normal);
                            else
                                PickAFight(FightDifficulty.Hard);
                        }
                        PickAFight(FightDifficulty.Boss);
                    }
                    break;
                    #endregion
            }
        }

        public void InitMonsters(List<Characters.CharacterData> monsterData)
        {
            foreach (Fight fight in fights)
                fight.InitMonster(monsterData);
        }

        private void PickAFight(FightDifficulty difficulty)
        {
            int n = 0;
            switch (difficulty)
            {
                case FightDifficulty.Easy:
                    n = Random.Range(0, easyFights.Count);
                    fights.Add(easyFights[n]);
                    easyFights.RemoveAt(n);
                    test += "easy ";
                    break;

                case FightDifficulty.Normal:
                    n = Random.Range(0, normalFights.Count);
                    fights.Add(normalFights[n]);
                    normalFights.RemoveAt(n);
                    test += "normal ";
                    break;

                case FightDifficulty.Hard:
                    n = Random.Range(0, hardFights.Count);
                    fights.Add(hardFights[n]);
                    hardFights.RemoveAt(n);
                    test += "hard ";
                    break;

                case FightDifficulty.Boss:
                    n = Random.Range(0, bossFights.Count);
                    fights.Add(bossFights[n]);
                    bossFights.RemoveAt(n);
                    test += "boss ";
                    break;
            }
        }
    }
}
