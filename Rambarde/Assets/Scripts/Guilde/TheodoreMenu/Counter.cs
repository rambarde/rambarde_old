using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public int maxCount;
    private int currentCount;
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        resetCounter();
        updateText();
    }

    public void resetCounter()
    {
        if (currentCount > 0)
        {
            button.interactable = false;
            currentCount = 0;
            updateText();
        }
    }

    public void increment()
    {
        if (currentCount < maxCount)
        {
            button.interactable = true;
            currentCount += 1;
            updateText();
        }
    }

    private void updateText()
    {
        GetComponent<Text>().text = currentCount.ToString() + " / " + maxCount.ToString();
    }
}
