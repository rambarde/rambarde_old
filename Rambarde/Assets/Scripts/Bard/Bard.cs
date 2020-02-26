using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Melodies;
using UniRx;
using UnityEngine;

namespace Bard {
    public class Bard : MonoBehaviour {
        [SerializeField] private int baseActionPoints;
        [SerializeField] private Transform m1, m2, m3, m4;

        public Hud hud;
        public Inspiration inspiration;
        public List<Instrument> instruments;
        public ReactiveProperty<int> actionPoints;
        public ReactiveProperty<int> maxActionPoints = new ReactiveProperty<int>(0);
        public ReactiveCollection<Melody> selectedMelodies = new ReactiveCollection<Melody>(new List<Melody>());

        private int _selectedInstrumentIndex;

        void Start() {
            inspiration = GetComponent<Inspiration>();
            actionPoints = new ReactiveProperty<int>(baseActionPoints);

            SetActionPlayableMelodies();
            hud.Init(this);
        }

        public void SelectMelody(Melody melody) {
            if (!instruments.Any(instrument => instrument.melodies.Contains(melody))) {
                Debug.Log("Warning : Melody [" + melody.name + "] is not equipped");
                return;
            }

            if (!melody.isPlayable.Value) {
                Debug.Log("Warning : Melody [" + melody.name + "] is not playable");
                return;
            }

            SetActionPlayableMelodies();

            melody.isPlayable.Value = false;

            if (_selectedInstrumentIndex == 0) {
                //if no instrument was selected
                //select melody's instrument
                _selectedInstrumentIndex = instruments.FindIndex(i => i.melodies.Contains(melody));
            }

            //if an instrument is selected
            if (_selectedInstrumentIndex != 0) {
                //make other instrument melodies unplayable
                int unselected = _selectedInstrumentIndex == 1 ? 2 : 1;
                foreach (Melody m in instruments[unselected].melodies) {
                    m.isPlayable.Value = false;
                }
            }

            selectedMelodies.Add(melody);
            actionPoints.Value -= melody.Size;

            inspiration.SelectMelody(melody);
            //musicPlanner.PlaceMelody(melody);
        }

        /**
         * Make melodies playable or not based on the action points
         */
        private void SetActionPlayableMelodies() {
            foreach (Instrument instrument in instruments) {
                foreach (Melody melody in instrument.melodies) {
                    melody.isPlayable.Value &= actionPoints.Value - melody.Size >= 0;
                }
            }
        }

        public void Reset() {
            // reset action points
            foreach (var melody in selectedMelodies) {
                inspiration.UnselectMelody(melody);
                actionPoints.Value += melody.Size;
            }

            selectedMelodies.Clear();

            //musicPlanner.Reset();
            _selectedInstrumentIndex = 0;
            foreach (Instrument instrument in instruments) {
                foreach (Melody melody in instrument.melodies) {
                    melody.isPlayable.Value = true;
                }
            }
        }

        public async void Done() {
            //musicPlanner.Done();
            foreach (Instrument instrument in instruments) {
                foreach (Melody melody in instrument.melodies) {
                    melody.isPlayable.Value = true;
                }
            }

            _selectedInstrumentIndex = 0;
            actionPoints.Value = baseActionPoints;
            await StartRhythmGame();
            await CombatManager.Instance.ExecTurn();
        }

        private struct Pair {
            public string data;
            public int i;

            public Pair(string data, int i) {
                this.i = i;
                this.data = data;
            }
        }

        private int beat = 1;

        private async Task StartRhythmGame() {
            foreach (var melody in selectedMelodies) {
                int n = 0;
                int i = 0;
                melody.Data.ToObservable()
                    .Where(x => x != '_')
                    .Select(x => {
                        if (i == melody.Data.Length - 1) {
                            n = 1;
                            return new Pair(x.ToString(), 1);
                        }

                        n = melody.Data.Substring(i + 1).TakeWhile(c => c == '_').Count() + 1;
                        var pair = new Pair(melody.Data.Substring(i, n), n);
                        i += n;
                        return pair;
                    })
                    .Zip(Observable.Timer(TimeSpan.FromSeconds(beat * n)).Repeat(), (x, _) => x)
                    .Subscribe(x => Debug.Log(x));
            }
        }

        private async Task SpawnMusicNote(char note) {
            Transform t = null;
            switch (note) {
                case '1':
                    t = m1;
                    break;
                case '2':
                    t = m2;
                    break;
                case '3':
                    t = m3;
                    break;
                case '4':
                    t = m4;
                    break;
            }

            var go = GameObject.CreatePrimitive(PrimitiveType.Cube);
            go.transform.parent = t;
            go.transform.localPosition = Vector3.zero;

            await Utils.AwaitObservable(Observable.Timer(TimeSpan.FromSeconds(1)));
        }
    }
}