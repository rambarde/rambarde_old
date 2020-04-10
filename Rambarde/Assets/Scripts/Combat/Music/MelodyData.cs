namespace Music {
    public class MelodyData {

        private string _data;
        private int _length;

        public MelodyData() {
            _length = 0;
            _data = "";
        }

        public MelodyData(string data) {
            _data = data;
            _length = data.Length;
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

        public override string ToString() {
            return _data;
        }
    }
}