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

        // Critical (percentage)
        public float crit;
    
        // Precision (percentage)
        public float prec;

        public void Init() {
            hp = new ReactiveProperty<float>(maxHp);
        }
    }
}