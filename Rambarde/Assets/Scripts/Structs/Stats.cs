using System;

namespace Structs {
    [Serializable]
    public struct Stats {
        public Stats(float end, float atq, float prot, float crit, float prec) {
            this.end = end;
            this.atq = atq;
            this.prot = prot;
            this.crit = crit;
            this.prec = prec;
        }

        // Endurance
        public float end;
        // public int End => _end;

        // Attack 
        public float atq;
        // public float Atq => _atq;

        // Protection (percentage)
        public float prot;
        // public float Prot => _prot;

        // Critical (percentage)
        public float crit;
        // public float Crit => _crit;
    
        // Precision (percentage)
        public float prec;
    }
}