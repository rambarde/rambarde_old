using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManagerBehaviour : MonoBehaviour
{
    public void Done()
    {
        GetComponent<GameManager>().ChangeScene(1);
    }
}
