using System.Threading.Tasks;
using Characters;
using UniRx;

namespace Status {
    public class HealthRegen : StatusEffect {
        private readonly float _pts;

        public HealthRegen(CharacterControl target, float pts, int turns) : base(target, turns) {
            type = EffectType.HealthRegen;
            _pts = pts;
            spriteName = "vfx-heal";
        }

        protected override async Task PreTurnStart() {
            await target.Heal(_pts);
        }
    }
}