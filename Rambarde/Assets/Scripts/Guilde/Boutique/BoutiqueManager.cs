using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoutiqueManager : MonoBehaviour
{
    public GameObject[] shops;

    public GameObject[] buttons;

    public List<Characters.Equipment> equipmentOwned; //useful for client menu (giving the equipment to clients)

    private void Start()
    {
        // weapon shop
        shops[0].SetActive(true);
        buttons[0].GetComponent<Button>().interactable = false;

        // armor shop
        shops[1].SetActive(false);
        buttons[1].GetComponent<Button>().interactable = true;

        // instrument shop
        shops[2].SetActive(false);
        buttons[2].GetComponent<Button>().interactable = true;
    }

    public void SwitchBoutique(int numShop)
    {
        for (int i = 0; i < shops.Length; i++)
        {
            if (i == numShop)
            {
                shops[i].SetActive(true);
                buttons[i].GetComponent<Button>().interactable = false;
            }
            else
            {
                shops[i].SetActive(false);
                buttons[i].GetComponent<Button>().interactable = true;
            }
        }
    }
}
