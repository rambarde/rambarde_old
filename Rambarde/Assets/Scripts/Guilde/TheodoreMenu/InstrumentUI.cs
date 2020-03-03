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
    public string instrumentPassif;              //quick description of skill effect
    public int skillID1; 
    public int skillID2; 
    public int skillID3; 
    public int skillID4; 
    public bool isDraggable = true;

    [SerializeField]

    public InstrumentUI() { }

    public InstrumentUI(int id, string name, int type, string effect, int id1, int id2, int id3, int id4,  bool drag)
    {
        ID = id;
        instrumentName = name;
        instrumentType = type;
        instrumentPassif = effect;
        skillID1 = id1;
        skillID2 = id2;
        skillID3 = id3;
        skillID4 = id4;
        isDraggable = drag;
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
        this.skillID1 = instrument.skillID1;
        this.skillID2 = instrument.skillID2;
        this.skillID3 = instrument.skillID3;
        this.skillID4 = instrument.skillID4;
        this.isDraggable = instrument.isDraggable;
    }
}
