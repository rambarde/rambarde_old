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

    // Start is called before the first frame update
    void Start()
    {
        switchMenuPanel = GameObject.Find("SwitchMenuPanel");
        switchMenuPanel.SetActive(false);

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

    public void SetQuest(Quest quest)
    {
        selectedQuest = quest;
        GameObject.Find("ChosenQuestName").GetComponent<Text>().text = selectedQuest.name;
    }

    public void SetTheodore(Melodies.Melody[] melodies, Bard.Instrument[] instruments)
    {
        this.innateMelodies = melodies;
        this.instruments = instruments;
    }

    public void SetClients(Characters.CharacterControl[] clients)
    {
        this.clients = clients;
        //for(int i = 0; i < clients.Length; i++)
        //{
        //    Debug.Log("");
        //    Debug.Log("Client: " + clients[i].clientName);
        //    Debug.Log("Team :" + clients[i].team);
        //    Debug.Log("Skills: ");
        //    for (int j = 0; j < clients[i].skillWheel.Length;j++)
        //        Debug.Log(clients[i].skillWheel[j]);
        //    Debug.Log("CharacterData: " + clients[i].characterData);
        //}
    }

    public void ValidClient()
    {
        return;
    }

    public void ValidTheodore()
    {
        return;
    }

    public void ValidQuest()
    {
        return;
    }
}
