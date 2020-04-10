using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteBoueuse : SpecialRules
{
    public RouteBoueuse(RulesTarget target = RulesTarget.EveryAlly, RulesWhen when = RulesWhen.CombatBeginning) : base(target, when)
    {
        type = RulesType.Route;
    }
}
