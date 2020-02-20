using UnityEngine;

namespace Bard
{
    public class Inspiration : MonoBehaviour
    {
        public int inspirationValue = 0, tier2minValue, tier3minValue;
        int estimatedValueConsumed = 0, estimatedValueAdded = 0;

        /**
         * Check if a melody can be played
         * */
        public bool MelodyCanBePlayed(Melody.Melody melody)
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
        public void SelectMelody(Melody.Melody melody)
        {
            //melody adding inspiration
            if(melody.InspirationValue > 0)
            {
                estimatedValueAdded += melody.InspirationValue;
            }
            //melody removing inspiration
             else if (MelodyCanBePlayed(melody))
            {
                estimatedValueConsumed += melody.InspirationValue;
            }
        }

        /**
         * Unselect a melody
         * */
        public void UnselectMelody(Melody.Melody melody)
        {
            //melody adding inspiration
            if (melody.InspirationValue > 0)
            {
                estimatedValueAdded -= melody.InspirationValue;
            }
            //melody removing inspiration
            else
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

        public void ResetTurnValues()
        {
            estimatedValueAdded = estimatedValueConsumed = 0;
        }
    }
}