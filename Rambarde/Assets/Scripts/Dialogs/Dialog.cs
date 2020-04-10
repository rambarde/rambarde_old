using System;
using System.Collections;
using System.Collections.Generic;
using Skills;
using UnityEngine;
using UnityEngine.Serialization;

[Flags]
public enum DialogFilter
{
    //phases
    None            = 0,
    CombatStart     = 1,
    Damage          = 1<<1,
    CriticalDamage  = 1<<2,
    Kill            = 1<<3,
    Heal            = 1<<4,
    Buff            = 1<<5,
    Unbuff          = 1<<6,
    Victory         = 1<<7,
    Travel          = 1<<8,
    //actions prop
    Atq             = 1<<9,
    Prot            = 1<<10,
    Crit            = 1<<11,
    Rushing         = 1<<12,
    Marked          = 1<<13,
    Dizzy           = 1<<14,
    Deaf            = 1<<15,
    Destabilized    = 1<<16,
    Poison          = 1<<17,
    Confuzed        = 1<<18,
    //target
    Clients         = 1<<19,
    FakeMonsters    = 1<<20,
    RealMonsters    = 1<<21,
}

[Serializable]
public struct DialogPhrase
{
    public DialogFilter filter;
    public string phrase;
}

[CreateAssetMenu(fileName = "Dialog", menuName = "Dialog")]
public class Dialog : ScriptableObject
{
    public Dialog commonDialog;
    public DialogPhrase[] dialogPhrases;

    public List<DialogPhrase> GetPhrases()
    {
        List<DialogPhrase> phrases = new List<DialogPhrase>();

        foreach (var phrase in dialogPhrases)
            phrases.Add(phrase);
        
        if (commonDialog != null)
            foreach (var phrase in commonDialog.dialogPhrases)
                phrases.Add(phrase);
        
        return phrases;
    }
}
