using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrumeSournoise : SpecialRules
{
    public BrumeSournoise(RulesTarget target = RulesTarget.OneRandomEnemy, RulesWhen when = RulesWhen.EachTurn) : base(target, when)
    {
        type = RulesType.Brume;
    }
}
