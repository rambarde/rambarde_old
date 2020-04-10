using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuBehaviour : MonoBehaviour
{
    public GameObject theodorePanel;
    public List<GameObject> clientPanels;
    public GameObject questPanel;
    //public Image test;

    private void Awake()
    {
        InitTheodorePanel();
        InitClient();
        InitQuest();
    }

    private enum Type
    {
        Melody = 0,
        Instrument = 1
    }

    private void InitTheodorePanel()
    {
        List<Bard.Instrument> instruments = GameManager.instruments;
        List<GameObject> innatePanel = new List<GameObject>();
        List<GameObject> instrumentPanel = new List<GameObject>();

        innatePanel.Add(theodorePanel.transform.GetChild(2).gameObject);
        innatePanel.Add(theodorePanel.transform.GetChild(3).gameObject);
        instrumentPanel.Add(theodorePanel.transform.GetChild(4).gameObject);
        instrumentPanel.Add(theodorePanel.transform.GetChild(5).gameObject);

        int i = 0;
        foreach(GameObject innate in innatePanel)
        {
            innate.transform.GetChild(0).GetComponent<MelodyBehaviour>().melody = instruments[0].melodies[i];
            innate.transform.GetChild(0).GetComponent<Image>().sprite = instruments[0].melodies[i].sprite;
            innate.transform.GetChild(0).GetComponent<Image>().color = instruments[0].melodies[i].color;

            innate.transform.GetChild(1).GetComponent<MelodyBehaviour>().melody = instruments[0].melodies[i + 1];
            innate.transform.GetChild(1).GetComponent<Image>().sprite = instruments[0].melodies[i + 1].sprite;
            innate.transform.GetChild(1).GetComponent<Image>().color = instruments[0].melodies[i + 1].color;

            i += 2;
        }

        i = 0;

        foreach (GameObject inst in instrumentPanel)
        {
            inst.transform.GetChild(0).GetComponent<InstrumentBehaviour>().instrument = instruments[i + 1];
            inst.transform.GetChild(0).GetComponent<Image>().sprite = instruments[i + 1].sprite;
            inst.transform.GetChild(0).GetComponent<Image>().color = instruments[i + 1].color;

            inst.transform.GetChild(1).GetComponent<MelodyBehaviour>().melody = instruments[i + 1].melodies[0];
            inst.transform.GetChild(1).GetComponent<Image>().sprite = instruments[i + 1].melodies[0].sprite;
            inst.transform.GetChild(1).GetComponent<Image>().color = new Color(1, 1, 1, 1);//instruments[i + 1].melodies[0].color;

            inst.transform.GetChild(2).GetComponent<MelodyBehaviour>().melody = instruments[i + 1].melodies[1];
            inst.transform.GetChild(2).GetComponent<Image>().sprite = instruments[i + 1].melodies[1].sprite;
            inst.transform.GetChild(2).GetComponent<Image>().color = new Color(1, 1, 1, 1);//instruments[i + 1].melodies[1].color;

            inst.transform.GetChild(3).GetComponent<MelodyBehaviour>().melody = instruments[i + 1].melodies[2];
            inst.transform.GetChild(3).GetComponent<Image>().sprite = instruments[i + 1].melodies[2].sprite;
            inst.transform.GetChild(3).GetComponent<Image>().color = new Color(1, 1, 1, 1);//instruments[i + 1].melodies[2].color;

            inst.transform.GetChild(4).GetComponent<MelodyBehaviour>().melody = instruments[i + 1].melodies[3];
            inst.transform.GetChild(4).GetComponent<Image>().sprite = instruments[i + 1].melodies[3].sprite;
            inst.transform.GetChild(4).GetComponent<Image>().color = new Color(1, 1, 1, 1);//instruments[i + 1].melodies[3].color;

            i++;
        }
    }

    private void InitClient()
    {
        List<Client> clients = GameManager.clients;
        for (int i = 0; i < clients.Count; i++)
        {
            Client client = clients[i];
            GameObject clientPanel = clientPanels[i];
            //client image
            //TO DO//

            //name
            clientPanel.transform.GetChild(2).GetComponent<Text>().text = client.Name;

            //class
            clientPanel.transform.GetChild(3).GetComponent<Text>().text = client.Character.name;

            //armor
            //TO DO//

            //weapon
            //TO DO// 

            //trait
            //TO DO//

            //stats
            clientPanel.transform.GetChild(7).GetChild(5).GetComponent<Text>().text = client.Character.baseStats.maxHp.ToString();
            clientPanel.transform.GetChild(7).GetChild(6).GetComponent<Text>().text = client.Character.baseStats.atq.ToString();
            clientPanel.transform.GetChild(7).GetChild(7).GetComponent<Text>().text = client.Character.baseStats.prot.ToString();
            clientPanel.transform.GetChild(7).GetChild(8).GetComponent<Text>().text = client.Character.baseStats.prec.ToString();
            clientPanel.transform.GetChild(7).GetChild(9).GetComponent<Text>().text = client.Character.baseStats.crit.ToString();

            //skills
            for (int j = 0; j < client.SkillWheel.Length; j++) 
            {
                Skills.Skill skill = client.Character.skills[client.SkillWheel[j]];
                clientPanel.transform.GetChild(8).GetChild(j).GetComponent<SkillBehaviour>().skill = skill;
                clientPanel.transform.GetChild(8).GetChild(j).GetComponent<Image>().sprite = skill.sprite;
                clientPanel.transform.GetChild(8).GetChild(j).GetComponent<Image>().color = new Color(1, 1, 1, 1);
            }
        }
    }

    private void InitQuest()
    {
        questPanel.transform.GetChild(1).GetChild(1).GetComponent<Text>().text = GameManager.quest.Pitch;
        questPanel.transform.GetChild(2).GetComponent<Image>().sprite = GameManager.quest.map;
    }
}
