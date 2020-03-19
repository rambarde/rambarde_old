using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildeManagerBehaviour : MonoBehaviour
{
    public GameObject[] subMenus;

    GameObject switchMenuPanel;

    bool menuAlreadyActive = false;

    /** out **/
    public Quest selectedQuest;
    public Melodies.Melody[] innateMelodies;
    public Bard.Instrument[] instruments;
    public Characters.CharacterControl[] clients;

    public bool[] menuValid;

    // Start is called before the first frame update
    void Start()
    {
        switchMenuPanel = GameObject.Find("SwitchMenuPanel");
        switchMenuPanel.SetActive(false);
        menuValid = new bool[3];

        foreach (GameObject menu in subMenus)
        {
            menu.SetActive(false);
        }
    }

    public void DisplayMenu(int menuID)
    {
        if (!menuAlreadyActive)
        {
            subMenus[menuID].SetActive(true);
            menuAlreadyActive = true;
            switchMenuPanel.SetActive(true);
        }
    }

    public void CloseMenu()
    {
        foreach (GameObject menu in subMenus)
        {
            menu.SetActive(false);
        }

        menuAlreadyActive = false;
        switchMenuPanel.SetActive(false);
    }

    public void SetClients(Characters.CharacterControl[] clients)
    {
        this.clients = clients;
        menuValid[0] = true;
        //for(int i = 0; i < clients.Length; i++)
        //{
        //    Debug.Log("");
        //    Debug.Log("Client: " + clients[i].clientName);
        //    Debug.Log("Team z:" + clients[i].team);
        //    Debug.Log("Skills: ");
        //    for (int j = 0; j < clients[i].skillWheel.Length;j++)
        //        Debug.Log(clients[i].skillWheel[j]);
        //    Debug.Log("CharacterData: " + clients[i].characterData);
        //}
    }

    public void SetQuest(Quest quest)
    {
        selectedQuest = quest;
        GameObject.Find("ChosenQuestName").GetComponent<Text>().text = selectedQuest.name;
        menuValid[1] = true;
    }

    public void SetTheodore(Melodies.Melody[] melodies, Bard.Instrument[] instruments)
    {
        this.innateMelodies = melodies;
        this.instruments = instruments;
        menuValid[2] = true;
    }

    public void resetClients() 
    {
        this.clients = null;
        menuValid[0] = false;
    }

    public void resetQuest()
    {
        menuValid[1] = false;
    }

    public void resetTheodore() 
    {
        this.innateMelodies = null;
        this.instruments = null;
        menuValid[2] = false;
    }
}
