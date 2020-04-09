using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChooseQuest : MonoBehaviour
{
    public Quest[] questPool;

    public int numberOfQuests;

    GameObject questMap;
    GameObject questDescription;

    int selectedQuestID = -1;

    GameObject goldManager;

    void Start()
    {
        // Only display the unlocked quests
        for(int i=numberOfQuests+1; i<=questPool.Length; i++)
        {
            GameObject.Find("QuestButton0" + i).SetActive(false);
        }

        // GameObjects that have to be changed depending on the selected quest
        questMap = GameObject.Find("QuestMap");
        questDescription = GameObject.Find("QuestDescription");

        // Label displaying how much gold the player has
        goldManager = GameObject.FindGameObjectWithTag("GoldLabel");
    }

    public void DisplayQuestMap(int quest_id)
    {
        // Display either normal quest are upgraded quest
        if (questPool[quest_id].isUpgradable)
        {
            questMap.GetComponent<Image>().sprite = questPool[quest_id].map;
        }
        else
        {
            questMap.GetComponent<Image>().sprite = questPool[quest_id].upgradedMap;
        }
        
        // Display quest description
        questDescription.GetComponent<Text>().text = questPool[quest_id].description;
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
            for (int i = 0; i < questPool.Length; i++)
            {
                questPool[i].isUpgradable = false;
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
}


