using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public ExpeditionMenu.Expedition quest;
    static public List<Client> clients; 
    static public List<Bard.Instrument> instruments;

    static public int gold = 300;

    static public int CurrentFight;
    static public bool QuestState;     //false = fights remaining; true = expedition is done

    // Change from combat Scene to Guilde Scene
    // 0: Guilde
    // 1: Combat
    // 2: Map/Between fights
    public void ChangeScene(int SceneToPlay)
    {
        SceneManager.LoadScene(SceneToPlay);
        Debug.Log("gold after exiting the guild : " + gold);
        Debug.Log("selected quest : " + quest.name);

        if (SceneToPlay == 1 && QuestState == true)
        {
            CurrentFight = 0;
            QuestState = false;
        }
    }

    public void ChangeCombat()
    {
        if(!(CurrentFight < quest.fightManager.fights.Count - 1))
        {
            QuestState = true;
            return;
        }

        //TODO: Map between fights here
        CurrentFight++;
        return;
    }

    public int CalculateGold()
    {
        int reward = 0;
        for(int i = 0; i < 3; i++)
        {
            int goldReward = quest.Gold;
            int alterReward = 0;    //add alternatif pathway reward
            float multiProgession = (float)quest.FightMax[i] / (float)quest.NumberOfFights;
            int randomReawrd = 0;   //add random events reward/losses
            int envyReward = 0;     //add envy rewards
            reward += (int)((goldReward + alterReward) * multiProgession) + envyReward;
            //Debug.Log("Client " + i + ": " + ((int)((goldReward + alterReward) * multiProgession) + envyReward) + " gold");
        }

        if (quest.FightMax[0] != quest.NumberOfFights & quest.FightMax[1] != quest.NumberOfFights & quest.FightMax[2] != quest.NumberOfFights)
            reward = (int)(reward / 2.0f);

        //Debug.Log("Gold total: " + reward);

        return reward;
    }
}
