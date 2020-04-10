using System.Threading.Tasks;
using Characters;

namespace Status {
    class Destabilized : StatusEffect {
        public Destabilized(CharacterControl target, int turns) : base(target, turns) {
            type = EffectType.Destabilized;
        }
        
        protected override Task Apply() {
            target.currentStats.prec *= 0.7f;

            return base.Apply();
        }
        
        protected override Task Remove() {
            target.currentStats.prec /= 0.7f;
            
            return base.Remove();
        }
    }
}