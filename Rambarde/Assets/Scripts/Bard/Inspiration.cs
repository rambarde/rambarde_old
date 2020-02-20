using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Melody
{
    public class Inspiration : MonoBehaviour
    {
        public int inspirationValue = 0, estimatedValueConsumed = 0, estimatedValueAdded = 0, tier2minValue, tier3minValue;
        List<Melody> selectedMelody = new List<Melody>();
        
        /**
         * Check if a melody can be played
         * */
        public bool MelodyCanBePlayed(Melody melody)
        {
            if (melody.Tier == 2 && inspirationValue < tier2minValue)
                return false;
            if (melody.Tier == 3 && inspirationValue < tier3minValue)
                return false;
            return estimatedValueConsumed + melody.InspirationValue > 0;
        }

        /**
         * Select a melody
         * */
        public void SelectMelody(Melody melody)
        {
            //melody adding inspiration
            if(melody.InspirationValue > 0)
            {
                selectedMelody.Add(melody);
                estimatedValueAdded += melody.InspirationValue;
            }
            //melody removing inspiration
             else if (MelodyCanBePlayed(melody))
            {
                selectedMelody.Add(melody);
                estimatedValueConsumed += melody.InspirationValue;
            }
        }

        /**
         * Unselect a melody
         * */
        public void UnselectMelody(Melody melody)
        {
            //melody adding inspiration
            if (melody.InspirationValue > 0 && selectedMelody.Remove(melody))
            {
                estimatedValueAdded -= melody.InspirationValue;
            }
            //melody removing inspiration
            else if (selectedMelody.Remove(melody))
            {
                estimatedValueConsumed -= melody.InspirationValue;
            }
        }

        /**
         * Affect inspiration with melody played (end phase)
         * */
        public void PlayMelodies()
        {
            inspirationValue += estimatedValueAdded + estimatedValueConsumed;
            ResetTurnValues();
        }

        void ResetTurnValues()
        {
            estimatedValueAdded = estimatedValueConsumed = 0;
            selectedMelody.Clear();
        }
    }
}