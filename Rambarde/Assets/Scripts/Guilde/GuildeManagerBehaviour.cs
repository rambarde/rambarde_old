using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GuildeManagerBehaviour : MonoBehaviour
{
    public GameObject[] subMenus;
    public Bard.Instrument BaseInstrument;

    GameObject switchMenuPanel;

    GameObject signQuest;
    GameObject signClient;
    GameObject signTheodore;

    bool menuAlreadyActive = false;

    /** out **/
    public ExpeditionMenu.Expedition selectedQuest;
    public List<Bard.Instrument> instruments;
    public List<Client> clients;

    public bool[] menuValid;

    // Start is called before the first frame update
    void Start()
    {
        switchMenuPanel = GameObject.Find("SwitchMenuPanel");
        switchMenuPanel.SetActive(false);
        menuValid = new bool[3];

        signQuest = GameObject.Find("exPointQuest");
        if (!(selectedQuest == null))
        {
            signQuest.SetActive(false);
        }

        signClient = GameObject.Find("exPointClient");
        signTheodore = GameObject.Find("exPointTheodore");

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

    public void SetClients(List<Client> clients)
    {
        this.clients = clients;
        menuValid[0] = true;
        signClient.SetActive(false);
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

    public void SetQuest(ExpeditionMenu.Expedition quest)
    {
        selectedQuest = quest;
        //GameManager.quest = quest;
        signQuest.SetActive(false);
        menuValid[1] = true;
    }

    public void SetTheodore(Melodies.Melody[] melodies, List<Bard.Instrument> instruments)
    {
        for (int i = 0; i < melodies.Length; i++)
            BaseInstrument.melodies[i] = melodies[i];

        instruments.Insert(0, BaseInstrument);
        this.instruments = instruments;
        menuValid[2] = true;
        signTheodore.SetActive(false);
    }

    public void resetClients() 
    {
        this.clients = null;
        menuValid[0] = false;
        signClient.SetActive(true);
    }

    public void resetQuest()
    {
        menuValid[1] = false;
        signQuest.SetActive(true);
    }

    public void resetTheodore() 
    {
        this.instruments = null;
        menuValid[2] = false;
        signTheodore.SetActive(true);
    }
}
