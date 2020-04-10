using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldValue : MonoBehaviour
{
    int goldValue;
    GameObject goldLabel;

    GameObject noGoldPanel;

    // Start is called before the first frame update
    void Start()
    {
        // Set gold Value to the static value from GameManager
        goldValue = GameManager.gold;

        goldLabel = GameObject.FindGameObjectWithTag("GoldLabel");
        goldLabel.GetComponent<Text>().text = goldValue.ToString() + "G";

        // Deactivate NoGoldPanel
        noGoldPanel = GameObject.FindGameObjectWithTag("NoGoldPanel");
        noGoldPanel.SetActive(false);
    }

    public bool HasEnoughGold(int price)
    {
        return goldValue >= price;
    }

    public void Pay(int price)
    {
        goldValue -= price;
        goldLabel.GetComponent<Text>().text = goldValue.ToString() + "G";

        // Update gold in GameManager
        GameManager.gold = goldValue;
    }

    public void DisplayNoGoldMessage()
    {
        noGoldPanel.SetActive(true);
        if (GameObject.Find("Scroll Parcours") != null)
            GameObject.Find("Scroll Parcours").GetComponent<ScrollRect>().enabled = false;

        GameObject[] buttons = GameObject.FindGameObjectsWithTag("SelectButton");
        foreach(GameObject button in buttons)
        {
            button.GetComponent<Button>().interactable = false;
        }

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
        if (GameObject.Find("Scroll Parcours") != null)
            GameObject.Find("Scroll Parcours").GetComponent<ScrollRect>().enabled = true;

        GameObject[] buttons = GameObject.FindGameObjectsWithTag("SelectButton");
        foreach (GameObject button in buttons)
        {
            button.GetComponent<Button>().interactable = true;
        }

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
