using System;
using System.Threading.Tasks;
using Characters;
using UniRx;

namespace Status {
    public class PoisonEffect : StatusEffect {
        private readonly float _dmg;
        private string _animationName = "PoisonEffect";

        public PoisonEffect(CharacterControl target, float dmg, int turns) {
            _dmg = dmg;
            Target = target;
            spriteName = "vfx-poison";
            turnsLeft = new ReactiveProperty<int>(turns);
        }

        protected override async Task PreTurnStart() {
            await Observable.Timer(TimeSpan.FromSeconds(1));
            await Target.TakeDamage(_dmg);
        }

        protected override Task PostTurnEnd() {
            return null;
        }
    }
}