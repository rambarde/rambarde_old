using TMPro;
using UniRx;

namespace Characters {
    public class CharacterVfx {
        private Character _character;
        private TextMeshProUGUI _characterHealth;
        
        public CharacterVfx(Character character) {
            _character = character;
            _characterHealth = character.GetComponentInChildren<TextMeshProUGUI>();
            if (_characterHealth != null) {
                character.stats.end.AsObservable().Subscribe(x => _characterHealth.text = x.ToString());
            }
        }
    }
}