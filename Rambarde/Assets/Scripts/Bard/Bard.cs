using System.Collections.Generic;
using System.Linq;
using Melodies;
using UniRx;
using UnityEngine;

namespace Bard {
    public class Bard : MonoBehaviour {

        public Inspiration inspiration;
        public MusicPlanner musicPlanner;
        public List<Instrument> instruments;

        public ReactiveProperty<int> actionPoints = new ReactiveProperty<int>(0);
        public ReactiveProperty<int> maxActionPoints = new ReactiveProperty<int>(0);
        public ReactiveProperty<List<Melody>> selectedMelodies = new  ReactiveProperty<List<Melody>>(new List<Melody>());

        private int _selectedInstrumentIndex = 0;
        
        void Start() {
            inspiration = GetComponent<Inspiration>();
            musicPlanner = GetComponent<MusicPlanner>();
        }

        public void SelectMelody(Melody melody) {
            if (! instruments.Any(instrument => instrument.melodies.Contains(melody))) {
                Debug.LogWarning("Melody [" + melody.name +"] is not equipped");
                return;
            }

            if (! melody.isPlayable.Value) {
                Debug.LogWarning("Melody [" + melody.name + "] is not playable");
                return;
            }

            SetActionPlayableMelodies();
            
            melody.isPlayable.Value = false;
            
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

            selectedMelodies.Value.Add(melody);
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
                    melody.isPlayable.Value = actionPoints.Value - melody.Size >= 0;
                }
            }
        }

        public void Reset() {
            // reset action points
            foreach (var melody in selectedMelodies.Value) {
                inspiration.UnselectMelody(melody);
                actionPoints.Value += melody.Size;
            }
            
            selectedMelodies.Value.Clear();
            
            //musicPlanner.Reset();
            _selectedInstrumentIndex = 0;
            SetActionPlayableMelodies();
        }

        public void Done() {
            //musicPlanner.Done();
            ExecTurn();
            CombatManager.Instance.ExecTurn();
        }

        private void ExecTurn() {
            foreach (var melody in selectedMelodies.Value) {
                melody.Execute(null);
            }
        }

    }
}
