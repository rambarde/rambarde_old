using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayMenuOnClick : MonoBehaviour
{
    /* Values for MenuID:
     * 0 : Expedition Menu
     * 1 : Client Menu
     * 2 : Theodore Menu
     * 3 : Boutique
     */
    public int MenuID;

    void OnMouseDown()
    {
        GameObject.Find("GuildeMenu").GetComponent<GuildeManagerBehaviour>().DisplayMenu(MenuID);
    }
}
