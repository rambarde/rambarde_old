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
            _dmg = dmg;
            Target = target;
            spriteName = "vfx-poison";
            TurnsLeft = new ReactiveProperty<int>(turns);
        }

        public override void ApplyEffect() { }

        public override void RemoveEffect() {
            Target.statusEffects.Remove(this);
            Target.statusEffects.Remove(this);
        }

        public override async Task TurnStart() {
            await Observable.Timer(TimeSpan.FromSeconds(1));
            await Target.TakeDamage(_dmg);
            Debug.Log("Damage inflicted: " + _dmg);
        }

        protected override async Task PreTurnEnd() {
            Debug.Log(Target);
        }
    }
}