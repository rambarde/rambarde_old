using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public int maxCount;
    private int _currentCount;
    public int CurrentCount { get { return _currentCount; } set { _currentCount = value; } }
    public Button button;

    // Start is called before the first frame update
    void Start()
    {
        resetCounter(); 
    }

    public void resetCounter()
    {
        if (_currentCount > 0)
        {
            button.interactable = false;
            _currentCount = 0;
        }
        updateText();
    }

    public void increment()
    {
        if (_currentCount < maxCount)
        {
            button.interactable = true;
            _currentCount += 1;
            updateText();
        }
    }

    private void updateText()
    {
        GetComponent<Text>().text = _currentCount.ToString() + " / " + maxCount.ToString();
    }
}
