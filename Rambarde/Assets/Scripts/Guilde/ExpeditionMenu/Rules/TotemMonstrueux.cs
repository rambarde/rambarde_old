using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TotemMonstrueux : SpecialRules
{
    public TotemMonstrueux(RulesTarget target = RulesTarget.EveryEnemy, RulesWhen when = RulesWhen.CombatBeginning) : base(target, when)
    {
        type = RulesType.Totem;
    }
}
