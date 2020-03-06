using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkillUI : MonoBehaviour
{
    public int ID;                          //skill ID
    public string skillName;                //skill name
    public int skillTier;                   //could be 0=innate, 1=instrument, 2=trance skill
    public int inspirationCost;             //only for T2/T3 skills
    public int tranceCost;                  //only for trance skills
    public int inspirationGeneration;       //only for T1 skills
    public int tranceGeneration;            //except for trance skills
    public string skillEffect;              //quick description of skill effect
    public bool isClickable = false;

    [SerializeField]

    public SkillUI() { }

    public SkillUI(int id, string name, int tier, int inspCost, int tranCost, int inspGen, int tranceGen, string effect, bool click)
    {
        ID = id;
        skillName = name;
        skillTier = tier;
        inspirationCost = inspCost;
        tranceCost = tranCost;
        inspirationGeneration = inspGen;
        tranceGeneration = tranceGen;
        skillEffect = effect;
        isClickable = click;
    }

    public SkillUI getCopy()
    {
        return (SkillUI)this.MemberwiseClone();
    }

    public void equip(SkillUI skill)
    {
        this.ID = skill.ID;
        this.skillName = skill.skillName;
        this.skillTier = skill.skillTier;
        this.inspirationCost = skill.inspirationCost;
        this.tranceCost = skill.tranceCost;
        this.inspirationGeneration = skill.inspirationGeneration;
        this.skillEffect = skill.skillEffect;
    }

    public void unEquip(int tier)
    {
        this.ID = 0;
        this.skillName = "";
        this.skillTier = tier;
        this.inspirationCost = 0;
        this.tranceCost = 0;
        this.inspirationGeneration = 0;
        this.skillEffect = "";
        this.isClickable = false;
    }

    public void setClickable(bool clickable) { this.isClickable = clickable; }
}
