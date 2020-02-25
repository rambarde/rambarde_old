using System.Collections.Generic;
using System.Linq;
using Melodies;
using UnityEngine;

namespace Bard {
    public class Bard : MonoBehaviour {

        public Inspiration inspiration;
        public MusicPlanner musicPlanner;
        public List<Instrument> instruments;
        public List<Melody> selectedMelodies;

        void Start() {
            inspiration = GetComponent<Inspiration>();
            musicPlanner = GetComponent<MusicPlanner>();
        }

        public void SelectMelody(Melody melody) {
            if (! instruments.Any(instrument => instrument.melodies.Contains(melody))) {
                Debug.LogWarning("Melody [" + melody.name +"] is not equipped");
                return;
            }

            selectedMelodies.Add(melody);
            
            inspiration.SelectMelody(melody);
            musicPlanner.PlaceMelody(melody);
        }

        public void Reset() {
            foreach (var melody in selectedMelodies) {
                inspiration.UnselectMelody(melody);
            }
            selectedMelodies.Clear();
            musicPlanner.Reset();
        }

        public void Done() {
            musicPlanner.Done();
            
            CombatManager.Instance.ExecTurn();
        }

        public void ExecTurn() {
            foreach (var melody in selectedMelodies) {
                melody.Execute(null);
            }
        }

    }
}
