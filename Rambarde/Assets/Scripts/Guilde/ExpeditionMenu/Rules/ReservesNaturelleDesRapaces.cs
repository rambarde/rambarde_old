using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReservesNaturelleDesRapaces : SpecialRules
{
    public ReservesNaturelleDesRapaces(RulesTarget target = RulesTarget.OneRandomAlly, RulesWhen when = RulesWhen.EachTurn) : base(target, when)
    {
        type = RulesType.Reserve;
    }
}
