using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DisplayMenuOnClick : MonoBehaviour
{
    public int MenuID;

    void OnMouseDown()
    {
        GameObject.Find("GuildeMenu").GetComponent<GuildeManagerBehaviour>().DisplayMenu(MenuID);
    }
}
