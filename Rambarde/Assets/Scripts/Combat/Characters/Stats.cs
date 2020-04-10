using System;
using UniRx;

namespace Characters {
    [Serializable]
    public struct Stats {
        // Endurance
        public float maxHp;
        public ReactiveProperty<float> hp;

        // Attack 
        public float atq;

        // Protection (percentage)
        public float prot;

        // Precision (percentage)
        public float prec;

        // Critical (percentage)
        public float crit;
    
        public void Init() {
            hp = new ReactiveProperty<float>(maxHp);
        }
    }
}