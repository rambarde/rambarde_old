using System.Threading.Tasks;
using Characters;
using UniRx;

namespace Status {
    public class HealthRegen : StatusEffect {
        private readonly float _pts;

        public HealthRegen(CharacterControl target, float pts, int turns) {
            type = EffectType.HealthRegen;
            Target = target;
            _pts = pts;
            turnsLeft = new ReactiveProperty<int>(turns);
            spriteName = "vfx-heal";
        }

        protected override async Task PreTurnStart() {
            await Target.Heal(_pts);
        }
    }
}