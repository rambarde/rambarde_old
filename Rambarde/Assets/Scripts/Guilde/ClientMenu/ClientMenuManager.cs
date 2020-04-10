using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientMenuManager : MonoBehaviour
{
    public Characters.CharacterData[] classList;
    [SerializeField]
    private int _selectedClient;
    public int SelectedClient
    {
        get { return _selectedClient; }
        set
        {
            _selectedClient = value;
            if (value >= 3)
                doneButton.GetComponent<Button>().interactable = true;
            else
                doneButton.GetComponent<Button>().interactable = false;
        }
    }

    ClientBehaviour[] clientList;
    Counter counter;
    protected GameObject doneButton;

    void Awake()
    {
        clientList = transform.GetChild(2).GetComponentsInChildren<ClientBehaviour>();
        counter = transform.GetComponentInChildren<Counter>();
        GenerateClients();
        doneButton = transform.GetChild(transform.childCount - 1).gameObject;
        doneButton.GetComponent<Button>().interactable = false;
    }

    public void resetSelectedClient(int nClient) { SelectedClient -= nClient; }

    public void resetClientMenu()
    {
        if (transform.GetComponentInParent<GuildeManagerBehaviour>().menuValid[0] && SelectedClient >= 3)
            return;

        counter.resetCounter();
        foreach (ClientBehaviour client in clientList)
            client.ResetSelected();
        transform.GetComponentInParent<GuildeManagerBehaviour>().resetClients();
    }

    public void selectClients()
    {
        List<Client> clients = new List<Client>();
        for (int i = 0; i < clientList.Length; i++)
        {
            if (!clientList[i].IsClickable)
            {
/////// HEAD
                clients.Add(clientList[i].client);
///////
                //ClientBehaviour currentClient = clientList[i];
                //Characters.CharacterControl client = new Characters.CharacterControl();
                //Skills.Skill[] temp = new Skills.Skill[4];

                //for (int j = 0; j < currentClient.SkillWheel.Length; j++)
                //    temp[j] = currentClient.Character.skills[currentClient.SkillWheel[j]];

                //client.clientName = currentClient.ClientName;
                //client.team = Characters.Team.PlayerTeam;
                //client.skillWheel = temp;
                //client.characterData = currentClient.Character;

                //// client equipment = base equipment from class
                //client.equipment = new Characters.Equipment[2];
                //client.equipment[0] = currentClient.Character.baseEquipment[0];     //weapon
                //client.equipment[1] = currentClient.Character.baseEquipment[1];     //armor

                //clients[nClient] = client;
                //nClient++;
////// origin/develop-guilde
            }
        }

        transform.GetComponentInParent<GuildeManagerBehaviour>().SetClients(clients);
    }

    #region ClientsGeneration
    public void GenerateClients()
    {
        int[] classes = GenerateClasses();
        int[][] skills = GenerateSkillWheel();

        for(int i = 0; i < clientList.Length; i++)
            clientList[i].client = new Client(classList[classes[i]], skills[i], "NomDebug");
    }

    private int[] GenerateClasses()
    {
        List<int> possibleClasses = new List<int>() { 0, 1, 2, 3, 4, 5 };
        List<int> classOccurence = new List<int>() { 0, 0, 0, 0, 0, 0 };
        int[] chosenClasses = new int[6];

        for (int i = 0; i < 6; i++) 
        {
            int n = Random.Range(0, possibleClasses.Count);
            classOccurence[n] += 1;
            chosenClasses[i] = possibleClasses[n];

            if (classOccurence[n] == 3)
            {
                classOccurence.RemoveAt(n);
                possibleClasses.RemoveAt(n);
            }
        }
        return chosenClasses;
    }
    private int[][] GenerateSkillWheel()
    {
        int[][] skillWheel = new int[6][];
        for (int i = 0; i < skillWheel.Length; i++)
            skillWheel[i] = new int[4];
        for(int i = 0; i < skillWheel.Length; i++)
        {
            int[] currentSW = skillWheel[i];
            List<int> skillChoice = new List<int>() { 0, 1, 2, 3, 4, 5, 6, 7 };
            
            for (int j = 0; j < currentSW.Length; j++)
            {
                int n = Random.Range(0, skillChoice.Count);
                currentSW[j] = skillChoice[n];
                skillChoice.RemoveAt(n);
            }
        }

        return skillWheel;
    }
    #endregion
}
