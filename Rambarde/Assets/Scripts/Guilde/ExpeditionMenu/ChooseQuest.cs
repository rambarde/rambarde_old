using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseQuest : MonoBehaviour
{
    public List<ExpeditionMenu.Expedition> questPool;

    public int numberOfQuests;

    GameObject questMap;
    GameObject questDescription;

    int selectedQuestID = -1;

    GameObject goldManager;
    private List<List<int>> maps;

    void Start()
    {
        // Only display the unlocked quests
        for(int i=numberOfQuests+1; i<=questPool.Count; i++)
        {
            GameObject.Find("QuestButton0" + i).SetActive(false);
        }

        // GameObjects that have to be changed depending on the selected quest
        questMap = GameObject.Find("QuestMap");
        questDescription = GameObject.Find("QuestDescription");

        // Label displaying how much gold the player has
        goldManager = GameObject.FindGameObjectWithTag("GoldLabel");

        maps = GenerateMaps();
        ExpeditionPitchList pitchList = new ExpeditionPitchList();
        pitchList.Init();
        List<int> pitchIndex = GeneratePitch(questPool[0].type, questPool.Count);
        for(int i = 0; i < questPool.Count; i++)
        {
            Debug.Log("Init expe");
            questPool[i].Init();
            questPool[i].Pitch = pitchList.expeditionPitch(questPool[i].type, pitchIndex[i]);
        }
    }

    public void DisplayQuestMap(int quest_id)
    {
        // Display either normal quest are upgraded quest
        if (questPool[quest_id].IsUpgradable)
        {
            questMap.GetComponent<Image>().sprite = questPool[quest_id].map;
        }
        else
        {
            questMap.GetComponent<Image>().sprite = questPool[quest_id].upgradedMap;
        }
        
        // Display quest description
        questDescription.GetComponent<Text>().text = questPool[quest_id].Pitch;
        selectedQuestID = quest_id;

        // display button to select quest
        GameObject.Find("ExpeditionMenu").GetComponent<ExpeditionMenuBehaviour>().AllowQuestSelect(questPool[quest_id]);
    }

    public void UpgradeParcours()
    {
        if (goldManager.GetComponent<GoldValue>().HasEnoughGold(200)) //if enough gold (more or equal to 200G)
        {
            // pay the update and update gold label
            goldManager.GetComponent<GoldValue>().Pay(200); //pay 200G to upgrade Prcours

            // deactivate upgrade button
            GameObject.Find("UpgradeButton").SetActive(false);

            // Upgrade all Parcours quests
            for (int i = 0; i < questPool.Count; i++)
            {
                //questPool[i].isUpgradable = false;
                questPool[i].IsUpgradable = false;
                questPool[i].Gold = (int)(questPool[i].Gold * 1.5f);
            }

            // update currently displayed quest map if needed
            if (selectedQuestID != -1)
            {
                questMap.GetComponent<Image>().sprite = questPool[selectedQuestID].upgradedMap;
            }

        }
        else
        {
            Debug.Log("not enough gold to upgrade");
            goldManager.GetComponent<GoldValue>().DisplayNoGoldMessage();
        }
    }

    List<int> GeneratePitch(ExpeditionMenu.QuestType type, int nExpedition)
    {
        List<int> forestIndex = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        List<int> cryptIndex = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
        List<int> pitchIndex = new List<int>();

        switch (type)
        {
            case ExpeditionMenu.QuestType.Forest:
                for (int i = 0; i < nExpedition; i++)
                {
                    int n = Random.Range(0, forestIndex.Count);
                    pitchIndex.Add(forestIndex[n]);
                    forestIndex.RemoveAt(n);
                }
                break;

            case ExpeditionMenu.QuestType.Crypt:
                for (int i = 0; i < nExpedition; i++)
                {
                    int n = Random.Range(0, cryptIndex.Count);
                    pitchIndex.Add(cryptIndex[n]);
                    cryptIndex.RemoveAt(n);
                }
                break;
        }      
        return pitchIndex;
    }

    List<List<int>> GenerateMaps()
    {
        List<List<int>> generatedMaps = new List<List<int>>();
        List<int> level1 = new List<int>() { 0, 1, 2, 3 };
        List<int> level2 = new List<int>() { 0, 1, 2, 3, 4, 5 };

        for(int i = 0; i < questPool.Count; i++)
        {
            List<int> temp = new List<int>();
            int n1 = Random.Range(0, level1.Count);
            temp.Add(level1[n1]);
            level1.RemoveAt(n1);

            int n2 = Random.Range(0, level2.Count);
            temp.Add(level2[n2]);
            level2.RemoveAt(n2);

            generatedMaps.Add(temp);
        }

        return generatedMaps;
    }

    //TO DO after merging//
    //Sprite LoadMap(bool upgraded, int i)
    //{
    //    Sprite map;
    //    if (upgraded) { }
    //    else { }

    //    return map;
    //}
}


