using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InstrumentUI : MonoBehaviour
{
    public int ID;                          //skill ID
    public string instrumentName;           //skill name
    [Tooltip("0=ATK; 1=DEF; 2=HEAL")]
    public int instrumentType;              //could be 0=innate, 1=instrument, 2=trance skill
    public string instrumentPassif;         //quick description of skill effect
    public SkillUI skill1;
    public SkillUI skill2;
    public SkillUI skill3;
    public SkillUI skill4;
    public bool isClickable = true;

    [SerializeField]

    public InstrumentUI() { }

    public InstrumentUI(int id, string name, int type, string effect, SkillUI s1, SkillUI s2, SkillUI s3, SkillUI s4,  bool click)
    {
        ID = id;
        instrumentName = name;
        instrumentType = type;
        instrumentPassif = effect;
        skill1 = s1;
        skill2 = s2;
        skill3 = s3;
        skill4 = s4;
        isClickable = click;
    }

    public InstrumentUI getCopy()
    {
        return (InstrumentUI)this.MemberwiseClone();
    }

    public void equip(InstrumentUI instrument)
    {
        this.ID = instrument.ID;
        this.instrumentName = instrument.instrumentName;
        this.instrumentType = instrument.instrumentType;
        this.instrumentPassif = instrument.instrumentPassif;
        this.skill1 = instrument.skill1;
        this.skill2 = instrument.skill2;
        this.skill3 = instrument.skill3;
        this.skill4 = instrument.skill4;
    }

    public void unEquip()
    {
        this.ID = 0;
        this.instrumentName = "";
        this.instrumentType = 0;
        this.instrumentPassif = "";
        this.skill1 = null;
        this.skill2 = null;
        this.skill3 = null;
        this.skill4 = null;
        this.isClickable = false;
    }

    public void setClickable(bool clickable) { this.isClickable = clickable; }
}
