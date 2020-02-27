using System;
using System.Collections.Generic;
using System.Linq;
using Characters;
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
        public ReactiveProperty<int> maxActionPoints;
        public ReactiveCollection<Melody> selectedMelodies = new  ReactiveCollection<Melody>(new List<Melody>());

        private int _selectedInstrumentIndex;

        void Start() {
            inspiration = GetComponent<Inspiration>();
            actionPoints = new ReactiveProperty<int>(baseActionPoints);
            maxActionPoints = new ReactiveProperty<int>(baseActionPoints);

            SetActionPlayableMelodies();
            SetInspirationPlayableMelodies();
            hud.Init(this);
        }

        public void SelectMelody(Melody melody, CharacterControl target = null) {
            if (! instruments.Any(instrument => instrument.melodies.Contains(melody))) {
                Debug.Log("Warning : Melody [" + melody.name +"] is not equipped");
                return;
            }

            if (!melody.isPlayable.Value) {
                Debug.Log("Warning : Melody [" + melody.name + "] is not playable");
                return;
            }

            SetActionPlayableMelodies();
            SetInspirationPlayableMelodies();

            if (_selectedInstrumentIndex == 0) { //if no instrument was selected
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
            
            melody.isPlayable.Value = false;
            melody.target = target;
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

        private void SetInspirationPlayableMelodies() {
            foreach (Instrument instrument in instruments) {
                foreach (Melody melody in instrument.melodies) {
                    if (melody.isPlayable.Value) {
                        switch (melody.tier) { 
                            case 2:
                                melody.isPlayable.Value = inspiration.current.Value >= inspiration.tier2MinValue;
                                break;
                            case 3:
                                melody.isPlayable.Value = inspiration.current.Value >= inspiration.tier3MinValue;
                                break;
                        }
                    }
                }
            }
        }

        public void Undo() {
            // reset action points
            foreach (var melody in selectedMelodies) {
                inspiration.UnselectMelody(melody);
            }

            Reset();
        }

        private void Reset() {
            _selectedInstrumentIndex = 0;
            actionPoints.Value = maxActionPoints.Value;
            
            foreach (Instrument instrument in instruments) {
                foreach (Melody melody in instrument.melodies) {
                    melody.isPlayable.Value = true;
                }
            }
            selectedMelodies.Clear();
            
            SetActionPlayableMelodies();
            SetInspirationPlayableMelodies();
        }

        public async void Done() {
            inspiration.PlayMelodies();
            Reset();
            await StartRhythmGame();
            await CombatManager.Instance.ExecTurn();
        }


        private class Aggregate {
            public readonly string data;
            public readonly int melodyIndex;
            public int noteIndex;

            public Aggregate(string data, int i1, int i2 = 0) {
                this.data = data;
                melodyIndex = i1;
                noteIndex = i2;
            }

            public Aggregate SetNoteIndex(int i2) {
                noteIndex = i2;
                return this;
            }
        }


        private int _beat = 1;

        private async Task StartRhythmGame() {
            var melodyIndex = 0;
            var charIndex = 0;
            var melody = selectedMelodies
                         // Transform melody list to (string, index) pairs
                         .Select(x => new Aggregate(x.Data, melodyIndex++))
                         // Transform every char from every melody to Aggregate with character index in array
                         .SelectMany(x => x.data.Select(m => new Aggregate(m.ToString(), x.melodyIndex, charIndex++)));
            var melodyStr = string.Concat(selectedMelodies.SelectMany(x => x.Data));

            var obs = melody
                      .Select(x => {
                          switch (x.data) {
                              case "_":
                                  return new Aggregate("*", x.melodyIndex);
                              case "-":
                                  return new Aggregate("-", x.melodyIndex);
                              default:
                                  if (x.noteIndex == melodyStr.Length - 1) return x;

                                  var len = melodyStr.Substring(x.noteIndex + 1).TakeWhile(c => c == '_').Count() + 1;
                                  return new Aggregate(melodyStr.Substring(x.noteIndex, len), x.melodyIndex);
                          }
                      })
                      .ToList(); // Compute list elements before starting the timer

            await Utils.AwaitObservable(
                Observable.Timer(TimeSpan.FromSeconds(_beat))
                          .Repeat()
                          .Zip(obs.ToObservable(), (_, y) => y),
                SpawnMusicNote
            );
        }

        private void SpawnMusicNote(Aggregate note) {
            // TODO : spawn music note object with speed and compare note.data to current note,
            // note.melodyIndex represents which melody should be played
            Debug.Log(note.data + "  " + note.melodyIndex);
        }
    }
}