using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClientBehaviour : 
    MonoBehaviour,
    IPointerClickHandler,
    IPointerEnterHandler,
    IPointerExitHandler

{
    public Characters.CharacterData character;
    [SerializeField]
    private bool _selected;
    public bool Selected { get { return _selected; } set { _selected = value; } }

    GameObject ClientImage;
    GameObject Name;
    GameObject Trait;
    GameObject Class;
    GameObject statEnd;
    GameObject statAtq;
    GameObject statProt;
    GameObject statPrec;
    GameObject statCrit;
    GameObject Skills;
    GameObject counter;

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
        counter = GameObject.Find("ClientCounter");
    }

    void Start()
    {
        if (character != null)
        {
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
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Client " + character.clientName + " choisi");
        counter.GetComponent<Counter>().increment();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GetComponent<RectTransform>().SetAsLastSibling();
        GetComponent<RectTransform>().localScale = new Vector3(0.7f, 0.7f, 0.7f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GetComponent<RectTransform>().localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }
}
