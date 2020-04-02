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
}
