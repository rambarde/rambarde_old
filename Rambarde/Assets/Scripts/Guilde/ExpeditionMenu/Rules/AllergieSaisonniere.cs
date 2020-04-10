using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllergieSaisonniere : SpecialRules
{
    public AllergieSaisonniere(RulesTarget target = RulesTarget.OneRandomAlly, RulesWhen when = RulesWhen.CombatBeginning) : base(target, when)
    {
        type = RulesType.Allergie;
    }
}
