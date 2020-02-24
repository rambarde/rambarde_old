using System.Collections.Generic;
using Melodies;
using UnityEngine;

namespace Bard {
    public class Bard : MonoBehaviour {

        public Inspiration inspiration;
        public MusicPlanner musicPlanner;
        //instruments
        public List<Melody> selectedMelodies;

        void Start() {
            inspiration = GetComponent<Inspiration>();
            musicPlanner = GetComponent<MusicPlanner>();
        }

        public void SelectMelody(Melody melody) {
            //check if melody is in instruments
            
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

    }
}
