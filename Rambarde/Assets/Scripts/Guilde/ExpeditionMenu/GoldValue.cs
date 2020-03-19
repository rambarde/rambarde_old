using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldValue : MonoBehaviour
{
    int goldValue;
    GameObject goldLabel;

    GameObject noGoldPanel;

    //GameObject[] questButtons;

    // Start is called before the first frame update
    void Start()
    {
        // Set gold Value to the static value from GameManager
        goldValue = GameManager.gold;

        goldLabel = GameObject.FindGameObjectWithTag("GoldLabel");
        goldLabel.GetComponent<Text>().text = goldValue.ToString();

        // Deactivate NoGoldPanel
        noGoldPanel = GameObject.FindGameObjectWithTag("NoGoldPanel");
        noGoldPanel.SetActive(false);
    }

    public bool HasEnoughGold()
    {
        return goldValue >= 200;
    }

    public void Pay(int price)
    {
        goldValue -= price;
        goldLabel.GetComponent<Text>().text = goldValue.ToString();

        // Update gold in GameManager
        GameManager.gold = goldValue;
    }

    public void DisplayNoGoldMessage()
    {
        noGoldPanel.SetActive(true);
        GameObject.FindGameObjectWithTag("FOREST_Button").GetComponent<Button>().interactable = false;
        GameObject.FindGameObjectWithTag("CRYPT_Button").GetComponent<Button>().interactable = false;
        GameObject.Find("Scroll Parcours").GetComponent<ScrollRect>().enabled = false;

        foreach (GameObject pLocked in GameObject.FindGameObjectsWithTag("PNotUnlocked"))
        {
            pLocked.GetComponent<Button>().interactable = false;
        }

        GameObject[] questButtons = GameObject.FindGameObjectsWithTag("QuestButton");
        Debug.Log(questButtons.Length);
        foreach (GameObject qbutton in questButtons)
        {
            qbutton.GetComponent<Button>().interactable = false;
        }
    }

    public void ShutDownMessage()
    {
        noGoldPanel.SetActive(false);
        GameObject.FindGameObjectWithTag("FOREST_Button").GetComponent<Button>().interactable = true;
        GameObject.FindGameObjectWithTag("CRYPT_Button").GetComponent<Button>().interactable = true;
        GameObject.Find("Scroll Parcours").GetComponent<ScrollRect>().enabled = true;

        foreach (GameObject pLocked in GameObject.FindGameObjectsWithTag("PNotUnlocked"))
        {
            pLocked.GetComponent<Button>().interactable = true;
        }

        GameObject[] questButtons = GameObject.FindGameObjectsWithTag("QuestButton");
        foreach (GameObject qbutton in questButtons)
        {
            qbutton.GetComponent<Button>().interactable = true;
        }
    }
}
