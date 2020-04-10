using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectParcour : MonoBehaviour
{
    List<GameObject> questMenus = new List<GameObject>();

    // Start is called before the first frame update
    void Awake()
    {
        GameObject forestMenu = GameObject.FindGameObjectWithTag("FOREST");
        GameObject cryptMenu = GameObject.FindGameObjectWithTag("CRYPT");

        questMenus.Add(forestMenu);
        questMenus.Add(cryptMenu);

        //Debug.Log(questMenus.Count);

        // Deactivate all sub-menus 
        foreach (GameObject menu in questMenus)
        {
            menu.SetActive(false);
        }
    }

    public void DisplayQuests(string tag)
    {
        Debug.Log("button " + tag + " has been pressed");

        foreach (GameObject menu in questMenus)
        {
            if (menu.tag == tag)
            {
                menu.SetActive(true);
                menu.transform.position = GameObject.Find(tag + "_Button").transform.position + new Vector3Int(-190,0,0);
            }
            else
            {
                menu.SetActive(false);
            }
        }

        // Prevent from selecting a quest
        GameObject.Find("ExpeditionMenu").GetComponent<ExpeditionMenuBehaviour>().DeactivateQuestSelect();
    }

    public void ScrollParcours()
    {
        foreach (GameObject menu in questMenus)
        {
            menu.SetActive(false);
        }

        // Prevent from selecting a quest
        GameObject.Find("ExpeditionMenu").GetComponent<ExpeditionMenuBehaviour>().DeactivateQuestSelect();
    }

}
