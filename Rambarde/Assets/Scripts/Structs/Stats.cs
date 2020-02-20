using System;
using UniRx;

namespace Structs {
    [Serializable]
    public struct Stats {
        // Endurance
        public ReactiveProperty<float> end;
        public float baseEnd;

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