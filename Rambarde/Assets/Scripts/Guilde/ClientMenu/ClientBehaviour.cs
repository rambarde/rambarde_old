using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClientBehaviour : MonoBehaviour
{
    public Characters.CharacterData character;
    private GameObject ClientImage;
    private GameObject Name;
    private GameObject Trait;
    private GameObject Class;
    private GameObject statEnd;
    private GameObject statAtq;
    private GameObject statProt;
    private GameObject statPrec;
    private GameObject statCrit;
    private GameObject Skills;

    private void Awake()
    {
        ClientImage = transform.GetChild(1).gameObject;
        Name = transform.GetChild(2).gameObject;
        Trait = transform.GetChild(3).gameObject;
        Class = transform.GetChild(4).gameObject;
        statEnd = transform.GetChild(5).GetChild(5).gameObject;
        statAtq = transform.GetChild(5).GetChild(6).gameObject;
        statProt = transform.GetChild(5).GetChild(7).gameObject;
        statPrec = transform.GetChild(5).GetChild(8).gameObject;
        statCrit = transform.GetChild(5).GetChild(9).gameObject;
        Skills = transform.GetChild(6).gameObject;

        ClientImage.GetComponent<Image>().sprite = character.clientImage;
        Name.GetComponent<Text>().text = character.clientName;
        //Trait                                             ///add trait/envy to characterData????????
        Class.GetComponent<Text>().text = character.name;
        statEnd.GetComponent<Text>().text = character.baseStats.maxHp.ToString();
        statAtq.GetComponent<Text>().text = character.baseStats.atq.ToString();
        statProt.GetComponent<Text>().text = character.baseStats.prot + "%";
        statPrec.GetComponent<Text>().text = character.baseStats.prec + "%";
        statCrit.GetComponent<Text>().text = character.baseStats.crit + "%";

        for (int i = 0; i < Skills.transform.childCount; i++)
        {
            GameObject skill = Skills.transform.GetChild(i).gameObject;
            skill.GetComponent<SkillBehaviour>().skill = character.skills[i];
            skill.GetComponent<Image>().sprite = character.skills[i].sprite;
        }
    }

    void Start()
    {

    }
}
