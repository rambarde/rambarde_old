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
}
