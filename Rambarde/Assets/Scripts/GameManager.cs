using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static public Quest quest;
    static public int clients = 0;
    static public List<Bard.Instrument> instruments;

    static public int gold = 300;

    // Change from combat Scene to Guilde Scene
    // 0: Guilde
    // 1: Combat
    public void ChangeScene(int SceneToPlay)
    {
        SceneManager.LoadScene(SceneToPlay);
        Debug.Log("gold after exiting the guild : " + gold);
        Debug.Log("selected quest : " + quest.name);
    }
}
