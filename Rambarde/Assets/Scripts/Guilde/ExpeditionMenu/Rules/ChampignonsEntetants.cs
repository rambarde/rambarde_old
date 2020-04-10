using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChampignonsEntetants : SpecialRules
{
    public ChampignonsEntetants(RulesTarget target = RulesTarget.Everyone, RulesWhen when = RulesWhen.CombatBeginning) : base(target, when)
    {
        type = RulesType.Champignons;
    }
}
