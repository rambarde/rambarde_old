using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClientBehaviour : 
    MonoBehaviour,
    IPointerClickHandler,
    IPointerEnterHandler,
    IPointerExitHandler

{
    [SerializeField]
    private bool _isClickable;
    public bool IsClickable { get { return _isClickable; } set { _isClickable = value; } }

    public Client client;

    GameObject ClientImage;
    GameObject Name;
    GameObject Trait;
    GameObject Class;
    GameObject statEnd;
    GameObject statAtq;
    GameObject statProt;
    GameObject statPrec;
    GameObject statCrit;
    GameObject skills;
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
        skills = transform.GetChild(6).gameObject;
        counter = GameObject.Find("ClientCounter");
    }

    void Start()
    {
        if (client.Character != null)
        {
            ClientImage.GetComponent<Image>().sprite = client.Character.clientImage;
            Name.GetComponent<Text>().text = client.Name;
            //Trait                                             ///add trait/envy to characterData????????
            Class.GetComponent<Text>().text = client.Character.name;
            statEnd.GetComponent<Text>().text = client.Character.baseStats.maxHp.ToString();
            statAtq.GetComponent<Text>().text = client.Character.baseStats.atq.ToString();
            statProt.GetComponent<Text>().text = client.Character.baseStats.prot + "%";
            statPrec.GetComponent<Text>().text = client.Character.baseStats.prec + "%";
            statCrit.GetComponent<Text>().text = client.Character.baseStats.crit + "%";

            for (int i = 0; i < client.SkillWheel.Length; i++)
            {
                GameObject skill = skills.transform.GetChild(i).gameObject;
                skill.GetComponent<SkillBehaviour>().skill = client.Character.skills[client.SkillWheel[i]];
                skill.GetComponent<Image>().sprite = //client.Character.skills[client.SkillWheel[i]].sprite!=null ?
                    client.Character.skills[client.SkillWheel[i]].sprite; //: AssetDatabase.GetBuiltinExtraResource<Sprite>("UI/Skin/UISprite.psd");
            }
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsClickable) 
            return;

        if (counter.GetComponent<Counter>().CurrentCount >= 3)
            return;

        counter.GetComponent<Counter>().increment();
        transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 215f/255f, 0f);

        IsClickable = false;
        GameObject.Find("Reset Client").GetComponent<Button>().onClick.AddListener(ResetSelected);
        transform.parent.GetComponentInParent<ClientMenuManager>().SelectedClient += 1;
    }

    public void ResetSelected()
    {
        if (IsClickable)
            return;

        transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f);
        transform.parent.GetComponentInParent<ClientMenuManager>().resetSelectedClient(1);
        IsClickable = true;
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
