using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Characters {
    public class CharacterVfx : MonoBehaviour {
        [SerializeField] private Character character;
        private TextMeshProUGUI _characterHealth;
        
        [SerializeField] private GameObject greenBar;

        [SerializeField] private GameObject yellowBar;
        
        [SerializeField] private GameObject redBar;
        
        private const float LerpTime = 1f;
        
        private void Start() {
            Debug.Log(character.name);
            _characterHealth = GetComponentInChildren<TextMeshProUGUI>();
            if (_characterHealth != null) {
                character.stats.end.AsObservable().Subscribe(x => _characterHealth.text = x.ToString());
                if(greenBar)
                    character.stats.end.AsObservable().Pairwise().Subscribe(UpdateGreenBar);
            }
        }
        
        private void UpdateYellowBar(Pair<float> values) {
            float t = 0f, currentLerpTime = 0f;
            yellowBar.LateUpdateAsObservable()
                .TakeWhile(_ => currentLerpTime < LerpTime + Time.deltaTime)
                .Subscribe(_ => {
                    float f = Mathf.Lerp(values.Previous, values.Current, t / LerpTime);
                    yellowBar.GetComponent<Image>().fillAmount = f / 100f;
                    currentLerpTime += Time.deltaTime;
                    t = currentLerpTime / LerpTime;
                    t = Mathf.Sin(t * Mathf.PI * 0.5f);
                });
        }

        private void UpdateGreenBar(Pair<float> values) {
            float t = 0f, currentLerpTime = 0f;
            this.LateUpdateAsObservable()
                .TakeWhile(_ => currentLerpTime < LerpTime + 2 * Time.deltaTime)
                .DoOnCompleted(() => UpdateYellowBar(values))
                .Subscribe(_ => {
                    float f = Mathf.Lerp(values.Previous, values.Current, t / LerpTime);
                    greenBar.GetComponent<Image>().fillAmount = f / 100f;
                    currentLerpTime += 2 * Time.deltaTime;
                    t = currentLerpTime / LerpTime;
                    t = Mathf.Sin(t * Mathf.PI * 0.5f);
                    Debug.Log(t);
                });
        }


    }
}