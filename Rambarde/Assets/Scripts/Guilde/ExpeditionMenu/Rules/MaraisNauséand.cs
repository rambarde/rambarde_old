using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaraisNauséand : SpecialRules
{
    public MaraisNauséand(RulesTarget target = RulesTarget.EveryAlly, RulesWhen when = RulesWhen.CombatBeginning) : base(target, when)
    {
        type = RulesType.Marais;
    }
}
