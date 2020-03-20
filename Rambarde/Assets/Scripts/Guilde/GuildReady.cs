using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuildReady : MonoBehaviour
{
    private GuildeManagerBehaviour guildManager;

    private void Start()
    {
        guildManager = GameObject.Find("GuildeMenu").GetComponent<GuildeManagerBehaviour>();
    }

    private void OnMouseDown()
    {
        bool[] ready = guildManager.menuValid;
        bool orReady = ready[0] || ready[1] || ready[2];
        bool andReady = ready[0] && ready[1] && ready[2];

        if (!orReady)
            Debug.Log("Rien n'est sélectionné");
            //feedback visuel

        if (orReady & !andReady)
        {
            if (!ready[0])
                Debug.Log("Menu clients non valide");
                //feedback visuel

            if (!ready[1])
                Debug.Log("Menu expédition non valide");
                //feedback visuel

            if (!ready[2])
                Debug.Log("Menu Théodore non valide");
                //feedback visuel
        }
        if (andReady & orReady)
        {
            Debug.Log("C'est ok");
            //lance la scène combat ici
        }
    }
}
