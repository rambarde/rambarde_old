using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RulesTarget
{
    EveryAlly,
    EveryEnemy,
    OneRandomAlly,
    OneRandomEnemy,
    Everyone
}

public enum RulesWhen ///??
{
    CombatBeginning,
    EachTurn
}

public class SpecialRules
{
    public RulesTarget Target;
    public RulesWhen When;
    public Skills.SkillAction action;
    public RulesType type;

    public SpecialRules(RulesTarget target, RulesWhen when)
    {
        Target = target;
        When = when;
    }
}

public enum RulesType
{
    None = 0,
    Marais = 1 << 1,
    Totem = 1 << 2,
    Reserve = 1 << 3,
    Route = 1 << 4,
    Champignons = 1 << 5,
    Allergie = 1 << 6,
    Brume = 1 << 7,
    Ame = 1 << 8,
    Fete = 1 << 9,
    Coup = 1 << 10,
    Depouille = 1 << 11,
    Diplomatie = 1 << 12,
    Memorial = 1 << 13,
    Compte = 1 << 14
}
