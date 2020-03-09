using System;
using System.Threading.Tasks;
using Characters;
using UniRx;

namespace Status {
    public class PoisonEffect : StatusEffect {
        private readonly float _dmg;
        private string _animationName = "PoisonEffect";
        
        public PoisonEffect(CharacterControl target, float dmg, int turns) : base(target, turns) {
            type = EffectType.Poison;
            _dmg = dmg;
            spriteName = "vfx-poison";
        }

        protected override async Task PreTurnStart() {
            await Observable.Timer(TimeSpan.FromSeconds(1));
            await target.TakeDamage(_dmg);
        }
    }
}