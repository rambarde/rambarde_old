using System.Collections.Generic;
using System.Collections.Specialized;

namespace Partition {
    public class MelodyData {

        private string _data;

        public MelodyData() {
        }

        public void PushNote(char note) {
            _data += note;
        }

        public int GetNote(int time) {
            return _data[time];
        }
        
        
    }
}