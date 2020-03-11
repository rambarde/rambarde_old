using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Tooltip : MonoBehaviour
{
    private GameObject obj;

    private GameObject Name;
    private GameObject effect;
    private GameObject target;
    private GameObject inspiration;
    private GameObject trance;
    private GameObject type;

    private string baseCosts;
    private string baseGeneration;

    void Start()
    {
        Name = this.transform.GetChild(1).gameObject;
        effect = this.transform.GetChild(2).gameObject;
        target = this.transform.GetChild(3).gameObject;
        inspiration = this.transform.GetChild(4).gameObject;
        trance = this.transform.GetChild(5).gameObject;
        type = this.transform.GetChild(6).gameObject;

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
            transform.GetChild(0).gameObject.SetActive(true);

            if (obj.GetComponent<MelodyBehaviour>() != null)
            {
                Melodies.Melody melody = obj.GetComponent<MelodyBehaviour>().melody;
                inspiration.SetActive(true);
                trance.SetActive(true);
                target.SetActive(true);
                type.SetActive(true);

                Name.GetComponent<Text>().text = Utils.SplitPascalCase(melody.name);
                effect.GetComponent<Text>().text = melody.effect;
                inspiration.GetComponent<Text>().text = stringInspiration(melody);
                trance.GetComponent<Text>().text = stringTrance(melody);
                type.GetComponent<Text>().text = "Tier " + melody.tier;         //add trance melody possibility
                target.GetComponent<Text>().text = melodyTargetToString(melody.targetMode);
            }

            if (obj.GetComponent<InstrumentBehaviour>() != null)
            {
                Bard.Instrument instrument = obj.GetComponent<InstrumentBehaviour>().instrument;
                type.SetActive(true);

                Name.GetComponent<Text>().text = Utils.SplitPascalCase(instrument.name);
                effect.GetComponent<Text>().text = instrument.passif;
                type.GetComponent<Text>().text = instrument.type;
            }

            if (obj.GetComponent<SkillBehaviour>() != null)
            {
                Skills.Skill skill = obj.GetComponent<SkillBehaviour>().skill;
                target.SetActive(true);

                Name.GetComponent<Text>().text = Utils.SplitPascalCase(skill.name);
                effect.GetComponent<Text>().text = skill.description;
                target.GetComponent<Text>().text = skillTargetToString(skill.actions);
            }

        }
        else
        {
            Name.SetActive(false);
            effect.SetActive(false);
            target.SetActive(false);
            inspiration.SetActive(false);
            trance.SetActive(false);
            type.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    string melodyTargetToString(Bard.MelodyTargetMode targetMode)
    {
        string target = "";
        switch (targetMode)
        {
            case Bard.MelodyTargetMode.OneAlly:
                target = "Un Client";
                break;
            case Bard.MelodyTargetMode.OneEnemy:
                target = "Un Monstre";
                break;
            case Bard.MelodyTargetMode.EveryAlly:
                target = "Tous les clients";
                break;
            case Bard.MelodyTargetMode.EveryEnemy:
                target = "Tous les ennemis";
                break;
            case Bard.MelodyTargetMode.Anyone:
                target = "N'importe qui"; //?????????????
                break;
            case Bard.MelodyTargetMode.Everyone:
                target = "Tout le monde";
                break;
        }
        return target;
    }

    string skillTargetToString(Skills.SkillAction[] actions)
    {
        if (actions.Length == 0)
            return "";

        StringBuilder stringBuilder = new StringBuilder();

        foreach(Skills.SkillAction action in actions)
        {
            switch (action.targetMode)
            {
                case Skills.SkillTargetMode.OneAlly:
                    appendTarget(stringBuilder, "Un allié");
                    break;
                case Skills.SkillTargetMode.OneEnemy:
                    appendTarget(stringBuilder, "Un ennemi");
                    break;
                case Skills.SkillTargetMode.Self:
                    appendTarget(stringBuilder, "Soi-même");
                    break;
                case Skills.SkillTargetMode.EveryAlly:
                    appendTarget(stringBuilder, "Tous les alliés");
                    break;
                case Skills.SkillTargetMode.EveryEnemy:
                    appendTarget(stringBuilder, "Tous les ennemis");
                    break;
            }
        }
        return stringBuilder.ToString();
    }

    string stringInspiration(Melodies.Melody melody)
    {
        int inspi = melody.inspirationValue;
        string s_inspi;
        if (inspi > 0)
            s_inspi = baseGeneration + inspi + " d'inspiration";
        else
            s_inspi = baseCosts + -inspi + " d'inspiration";

        return s_inspi;
    }

    string stringTrance(Melodies.Melody melody)
    {
        int trance = melody.tranceValue;
        string s_trance;
        if (trance > 0)
            s_trance = baseGeneration + trance + " de transe";
        else
            s_trance = baseCosts + -trance + " de transe";

        return s_trance;
    }

    public void setObject(GameObject _object) { this.obj = _object; }

    void appendTarget(StringBuilder sb, string target)
    {
        if (!sb.ToString().Contains(target))
        {
            sb.Append(target);
            sb.AppendLine();
        }
    }
}
