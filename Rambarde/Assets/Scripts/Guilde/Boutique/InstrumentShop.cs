using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstrumentShop : MonoBehaviour
{
    public List<Bard.Instrument> instruments;
    public List<Button> instrumentsButtons;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i<instrumentsButtons.Count; i++)
        {
            Bard.Instrument instrument = instruments[i];
            string instruName = instrumentsButtons[i].name;
            GameObject.Find(instruName + "_Image").GetComponent<Image>().sprite = instrument.sprite;
            GameObject.Find(instruName + "_Image").GetComponent<Image>().color = instrument.color;
            GameObject.Find(instruName + "_Name").GetComponent<Text>().text = Utils.SplitPascalCase(instrument.name);
            GameObject.Find(instruName + "_Description").GetComponent<Text>().text = instrument.passif;
            GameObject.Find(instruName + "_Price").GetComponent<Text>().text = instrument.price + "G";

            for (int j = 0; j<4; j++)
            {
                GameObject.Find(instruName + "_Melody" + j.ToString()).GetComponent<Text>().text = Utils.SplitPascalCase(instrument.melodies[j].name) 
                                                                                                                    + " : " 
                                                                                                                    + instrument.melodies[j].effect;
            }

            UpdateInstrumentShop();
        }
    }

    void UpdateInstrumentShop()
    {
        for (int i = 0; i < instrumentsButtons.Count; i++)
        {
           if(instruments[i].owned)
            {
                instrumentsButtons[i].interactable = false;
            }
        }
    }

    public void PurchaseInstrument(Bard.Instrument instrument)
    {
        GameObject goldManager = GameObject.FindGameObjectWithTag("GoldLabel");
        if (goldManager.GetComponent<GoldValue>().HasEnoughGold(instrument.price))
        {
            goldManager.GetComponent<GoldValue>().Pay(instrument.price);
            instrument.owned = true;
            UpdateInstrumentShop();
        }
        else
        {
            goldManager.GetComponent<GoldValue>().DisplayNoGoldMessage();
        }
    }    
}
