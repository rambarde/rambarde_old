using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tooltip : MonoBehaviour
{
    [HideInInspector]
    public SkillUI skill;
    [HideInInspector]
    public InstrumentUI instrument;

    private GameObject Name;
    private GameObject effect;
    private GameObject skillCosts;
    private GameObject skillGeneration;

    private string baseCosts;
    private string baseGeneration;

    void Start()
    {
        Name = this.transform.GetChild(1).gameObject;
        effect = this.transform.GetChild(2).gameObject;
        skillCosts = this.transform.GetChild(3).gameObject;
        skillGeneration = this.transform.GetChild(4).gameObject;

        baseCosts = "Coûte\n";
        baseGeneration = "Génère\n";

        DeActivated();
    }

    public void Activated()
    {
        Name.SetActive(true);
        effect.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(true);

        if (skill != null)
        {
            skillCosts.SetActive(true);
            skillGeneration.SetActive(true);

            Name.GetComponent<Text>().text = skill.skillName;
            effect.GetComponent<Text>().text = skill.skillEffect;
            skillCosts.GetComponent<Text>().text = costs();
            skillGeneration.GetComponent<Text>().text = generations();
        }
        if (instrument != null) 
        {
            Name.GetComponent<Text>().text = instrument.instrumentName;
            effect.GetComponent<Text>().text = instrument.instrumentPassif;
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

    string costs()
    {
        if (skill.inspirationCost == 0 && skill.tranceCost == 0)
            return "Pas de coût";

        string cost = baseCosts;
        if (skill.inspirationCost != 0)
            cost += "Inspiration: " + skill.inspirationCost + "\n";

        if (skill.tranceCost != 0)
            cost += "Trance: " + skill.tranceCost + "\n";
        return cost;
    }

    string generations()
    {
        if (skill.inspirationGeneration == 0 && skill.tranceGeneration == 0)
            return "Ne génère rien";

        string generation = baseGeneration;
        if (skill.inspirationGeneration != 0)
            generation += skill.inspirationGeneration + " :Inspiration\n";

        if (skill.tranceGeneration != 0)
            generation += skill.tranceGeneration + " :Trance\n";
        return generation;
    }
}
