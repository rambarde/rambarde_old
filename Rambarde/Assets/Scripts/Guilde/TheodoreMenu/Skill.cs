using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Skill : MonoBehaviour
{
    public int ID;                          //skill ID
    public string skillName;                //skill name
    public int skillTier;                   //could be 0=innate, 1=instrument, 2=trance skill
    public int inspirationCost;             //only for T2/T3 skills
    public int tranceCost;                  //only for trance skills
    public int inspirationGeneration;       //only for T1 skills
    public int tranceGeneration;            //except for trance skills
    public string skillEffect;              //quick description of skill effect
    public bool isDraggable = true;

    [SerializeField]

    public Skill() { }

    public Skill(int id, string name, int tier, int inspCost, int tranCost, int inspGen, int tranceGen, string effect, bool drag)
    {
        ID = id;
        skillName = name;
        skillTier = tier;
        inspirationCost = inspCost;
        tranceCost = tranCost;
        inspirationGeneration = inspGen;
        tranceGeneration = tranceGen;
        skillEffect = effect;
        isDraggable = drag;
    }

    public Skill getCopy()
    {
        return (Skill)this.MemberwiseClone();
    }

    public void equip(Skill skill)
    {
        this.ID = skill.ID;
        this.skillName = skill.skillName;
        this.skillTier = skill.skillTier;
        this.inspirationCost = skill.inspirationCost;
        this.tranceCost = skill.tranceCost;
        this.inspirationGeneration = skill.inspirationGeneration;
        this.skillEffect = skill.skillEffect;
        this.isDraggable = skill.isDraggable;
    }
}
