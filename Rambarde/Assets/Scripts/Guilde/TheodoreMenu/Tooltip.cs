using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tooltip : MonoBehaviour
{
    private Melodies.Melody melody;
    private Bard.Instrument instrument;

    private GameObject Name;
    private GameObject effect;
    private GameObject skillCosts;
    private GameObject skillGeneration;

    private GameObject inspiration;
    private GameObject trance;
    private GameObject type;

    private string baseCosts;
    private string baseGeneration;

    void Start()
    {
        Name = this.transform.GetChild(1).gameObject;
        effect = this.transform.GetChild(2).gameObject;
        skillCosts = this.transform.GetChild(3).gameObject;
        skillGeneration = this.transform.GetChild(4).gameObject;
        inspiration = this.transform.GetChild(3).gameObject;
        trance = this.transform.GetChild(4).gameObject;
        type = this.transform.GetChild(5).gameObject;

        baseCosts = "Coûte: \n";
        baseGeneration = "Génère:\n";

        Activate(false);
    }

    public void Activate(bool m_bool)
    {
        if (m_bool)
        {
            Name.SetActive(true);
            effect.SetActive(true);
            type.SetActive(true);
            transform.GetChild(0).gameObject.SetActive(true);

            if (melody != null)
            {
                inspiration.SetActive(true);
                trance.SetActive(true);

                Name.GetComponent<Text>().text = Utils.SplitCamelCase(melody.name);
                effect.GetComponent<Text>().text = melody.effect;
                inspiration.GetComponent<Text>().text = stringInspiration();
                trance.GetComponent<Text>().text = stringTrance();
                type.GetComponent<Text>().text = "Tier " + melody.tier;         //ajouter trance melody possibility
            }
            if (instrument != null)
            {
                Name.GetComponent<Text>().text = Utils.SplitCamelCase(instrument.name);
                effect.GetComponent<Text>().text = instrument.passif;
                type.GetComponent<Text>().text = instrument.type;
            }

        }
        else
        {
            Name.SetActive(false);
            effect.SetActive(false);
            skillCosts.SetActive(false);
            skillGeneration.SetActive(false);
            inspiration.SetActive(false);
            trance.SetActive(false);
            type.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void DeActivated()
    {
        Name.SetActive(false);
        effect.SetActive(false);
        skillCosts.SetActive(false);
        skillGeneration.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);
    }

    string stringInspiration()
    {
        int inspi = melody.inspirationValue;
        string s_inspi;
        if (inspi > 0)
            s_inspi = baseGeneration + inspi + " d'inspiration";
        else
            s_inspi = baseCosts + -inspi + " d'inspiration";

        return s_inspi;
    }

    string stringTrance()
    {
        int trance = melody.tranceValue;
        string s_trance;
        if (trance > 0)
            s_trance = baseGeneration + trance + " de transe";
        else
            s_trance = baseCosts + -trance + " de transe";

        return s_trance;
    }

    public void setMelody(Melodies.Melody melody) {
        this.melody = melody;
        this.instrument = null;
    }

    public void setInstrument(Bard.Instrument instrument) {
        this.instrument = instrument;
        this.melody = null;
    }
}
