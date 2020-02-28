using Melodies;
using UniRx;
using UnityEngine;

namespace Bard
{
    public class Inspiration : MonoBehaviour {
        public ReactiveProperty<int> current = new ReactiveProperty<int>(0);
        public int maximumValue = 10;
        public int tier2MinValue, tier3MinValue;
        public ReactiveProperty<int> estimateConsume = new ReactiveProperty<int>(0);
        public ReactiveProperty<int> estimateAdd = new ReactiveProperty<int>(0);

        /**
         * Check if a melody can be played
         * */
        public bool MelodyCanBePlayed(Melody melody)
        {
            if (melody.tier == 2 && current.Value < tier2MinValue)
                return false;
            if (melody.tier == 3 && current.Value < tier3MinValue)
                return false;
            return estimateConsume.Value + melody.inspirationValue > 0;
        }

        /**
         * Select a melody
         * */
        public void SelectMelody(Melody melody)
        {
            //melody adding inspiration
            if(melody.inspirationValue > 0)
            {
                estimateAdd.Value += melody.inspirationValue;
            }
            //melody removing inspiration
             else if (melody.isPlayable.Value)
            {
                estimateConsume.Value += melody.inspirationValue;
            }
        }

        /**
         * Unselect a melody
         * */
        public void UnselectMelody(Melody melody)
        {
            //melody adding inspiration
            if (melody.inspirationValue > 0)
            {
                estimateAdd.Value -= melody.inspirationValue;
            }
            //melody removing inspiration
            else
            {
                estimateConsume.Value -= melody.inspirationValue;
            }
        }

        public void ResetTurnValues()
        {
            estimateAdd.Value = estimateConsume.Value = 0;
        }
    }
}