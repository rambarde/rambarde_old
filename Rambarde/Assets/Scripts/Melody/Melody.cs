using System.Collections.Generic;
using Characters;
using UnityEngine;

namespace Melody {
    public abstract class Melody : MonoBehaviour {

        [SerializeField] private string data;
        public string Data => data;
       
        [SerializeField] private string melodyName;
        public string Name => melodyName;

        public int Tier;
        public int InspirationValue;

        /**
         *  execute melody on targets.
         *  targets can have multiple characters
         */
        public abstract void Execute(List<Character> targets);

    }
}
