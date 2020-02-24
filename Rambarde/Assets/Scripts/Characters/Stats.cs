using System;
using UniRx;

namespace Characters {
    [Serializable]
    public struct Stats {
        // Endurance
        public float baseEnd;
        public ReactiveProperty<float> end;

        // Attack 
        public float atq;

        // Protection (percentage)
        public float prot;

        // Critical (percentage)
        public float crit;
    
        // Precision (percentage)
        public float prec;

        public void Init() {
            end = new ReactiveProperty<float>(baseEnd);
        }
    }
}