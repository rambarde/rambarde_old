using System;
using System.Threading.Tasks;
using Characters;
using UniRx;
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

        public override async Task TurnStart() {
            Target.TakeDamage(_dmg);
            
            await Utils.AwaitObservable(Observable.Timer(new TimeSpan(0, 0, 2)));
        }

        protected override void PreTurnEnd() {
            Debug.Log(Target);
        }
    }
}