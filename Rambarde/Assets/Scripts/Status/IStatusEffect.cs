using System.Collections;
using System.Threading.Tasks;

namespace Status {
    public interface IStatusEffect {
        void ApplyEffect();
        void RemoveEffect();
        void TurnEnd();
        Task TurnStart();
        void AddTurns(int n);
    }
}