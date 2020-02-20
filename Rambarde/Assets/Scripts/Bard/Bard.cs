using Melody;
using UnityEngine;

namespace Bard {
    public class Bard : MonoBehaviour {

        public Inspiration inspiration;
        public MusicPlanner musicPlanner;
        //instruments

        void Start() {
            inspiration = GetComponent<Inspiration>();
            musicPlanner = GetComponent<MusicPlanner>();
        }

        public void SelectMelody(Melody.Melody melody) {
            //check if melody is in instruments
            
            inspiration.SelectMelody(melody);
            musicPlanner.PlaceMelody(melody);
        }

        public void Reset() {
            inspiration.ResetTurnValues();
            musicPlanner.Reset();
        }

        public void Done() {
            musicPlanner.Done();
            
            CombatManager.Instance.ExecTurn();
        }

    }
}
