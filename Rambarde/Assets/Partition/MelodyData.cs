using System.Collections.Generic;
using System.Collections.Specialized;

namespace Partition {
    public class MelodyData {

        private string _data;
        private int _length;

        public MelodyData() {
            _length = 0;
            _data = "";
        }

        public void PushNote(char note) {
            _data += note;
            _length += 1;
        }

        public int GetNote(int time) {
            return _data[time];
        }
        

        public int Length {
            get => _length;
        }
        
    }
}