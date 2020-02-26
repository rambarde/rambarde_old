using Melodies;
using UniRx;
using UnityEngine;

namespace Bard
{
    public class Inspiration : MonoBehaviour {
        public ReactiveProperty<int> current = new ReactiveProperty<int>(0);
        public int tier2MinValue, tier3MinValue;
        public ReactiveProperty<int> estimateConsume = new ReactiveProperty<int>(0);
        public ReactiveProperty<int> estimateAdd = new ReactiveProperty<int>(0);

        /**
         * Check if a melody can be played
         * */
        public bool MelodyCanBePlayed(Melody melody)
        {
            if (melody.Tier == 2 && current.Value < tier2MinValue)
                return false;
            if (melody.Tier == 3 && current.Value < tier3MinValue)
                return false;
            return estimateConsume.Value + melody.InspirationValue > 0;
        }

        /**
         * Select a melody
         * */
        public void SelectMelody(Melody melody)
        {
            //melody adding inspiration
            if(melody.InspirationValue > 0)
            {
                estimateAdd.Value += melody.InspirationValue;
            }
            //melody removing inspiration
             else if (MelodyCanBePlayed(melody))
            {
                estimateConsume.Value += melody.InspirationValue;
            }
        }

        /**
         * Unselect a melody
         * */
        public void UnselectMelody(Melody melody)
        {
            //melody adding inspiration
            if (melody.InspirationValue > 0)
            {
                estimateAdd.Value -= melody.InspirationValue;
            }
            //melody removing inspiration
            else
            {
                estimateConsume.Value -= melody.InspirationValue;
            }
        }

        /**
         * Affect inspiration with melody played (end phase)
         * */
        public void PlayMelodies()
        {
            current.Value += estimateAdd.Value + estimateConsume.Value;
            ResetTurnValues();
        }

        public void ResetTurnValues()
        {
            estimateAdd.Value = estimateConsume.Value = 0;
        }
    }
}