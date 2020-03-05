using System.Threading.Tasks;
using Characters;

namespace Status {
    class Destabilized : StatusEffect {
        private float _removedPrec;
        
        public Destabilized(CharacterControl target, int turns) : base(target, turns) { }

        protected override Task Apply() {
            _removedPrec = 0.3f * target.currentStats.prec;
            target.currentStats.prec -= _removedPrec;
            return base.Apply();
        }

        protected override Task Remove() {
            target.currentStats.prec += _removedPrec;
            return base.Remove();
        }
    }
}