using System;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

namespace Characters {
    public class CharacterVfx : MonoBehaviour {
        [SerializeField] private Character character;
        [SerializeField] private GameObject greenBar;
        [SerializeField] private GameObject yellowBar;
        [SerializeField] private GameObject redBar;

        private TextMeshProUGUI _characterHealth;
        private const float LerpTime = 1f;

        private void Start() {
            Debug.Log(character.name);
            _characterHealth = GetComponentInChildren<TextMeshProUGUI>();
            if (_characterHealth == null) return;

            character.stats.end.AsObservable().Subscribe(x => _characterHealth.text = x.ToString());

            if (greenBar && yellowBar) {
                character.stats.end.AsObservable().Pairwise().Subscribe(x =>
                    Utils.UpdateGameObjectLerp(x, greenBar, 2, LerpTime, LerpHealthBar,
                        pair => Utils.UpdateGameObjectLerp(x, yellowBar, 1, LerpTime, LerpHealthBar, _ => { })
                    )
                );
            }
        }

        private void LerpHealthBar(Pair<float> pair, GameObject go, ref float curLerpTime, float speed, float lerpTime, ref float t) {
            float f = Mathf.Lerp(pair.Previous, pair.Current, t / LerpTime);
            go.GetComponent<Image>().fillAmount = f / 100f;
            curLerpTime += speed * Time.deltaTime;
            t = curLerpTime / LerpTime;
            t = Mathf.Sin(t * Mathf.PI * 0.5f);
        }

        // private void UpdateBar(Pair<float> values, GameObject bar, int speed, Action<Pair<float>> update) {
        //     float t = 0f, currentLerpTime = 0f;
        //     bar.LateUpdateAsObservable()
        //         .TakeWhile(_ => currentLerpTime < LerpTime + speed * Time.deltaTime)
        //         .DoOnCompleted(() => update(values))
        //         .Subscribe(_ => {
        //             float f = Mathf.Lerp(values.Previous, values.Current, t / LerpTime);
        //             bar.GetComponent<Image>().fillAmount = f / 100f;
        //             currentLerpTime += speed * Time.deltaTime;
        //             t = currentLerpTime / LerpTime;
        //             t = Mathf.Sin(t * Mathf.PI * 0.5f);
        //         });
        // }
    }
}