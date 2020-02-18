using System.Collections;
using Characters;
using UnityEngine;

namespace Status {
    public class PoisonEffect : StatusEffect {
        private readonly float _dmg;
        private string _animationName = "PoisonEffect";

        public PoisonEffect(Character target, float dmg, int turns) {
            Target = target;
            TurnsLeft = turns;
            _dmg = dmg;
        }

        public override void ApplyEffect() { }

        public override void RemoveEffect() {
            Target.StatusEffects.Remove(this);
        }

        public override IEnumerator TurnStart() {
            Debug.Log(Target.name);
            Target.TakeDamage(_dmg);
            yield return new WaitForSeconds(2);
            Debug.Log("DONE");
        }

        protected override void PreTurnEnd() {
            Debug.Log(Target);
        }
    }
}