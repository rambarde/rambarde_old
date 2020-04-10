using Characters;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI {
    public class CharacterVfx : MonoBehaviour {
        private CharacterControl _characterControl;
        public GameObject greenBar;
        public GameObject yellowBar;
        public GameObject statusEffects;
        public TextMeshProUGUI characterHealth;

        private const float LerpTime = 1f;
        private const string ResourcesDir = "CharacterVfx";


        public async void Init(CharacterControl characterControl)
        {
            _characterControl = characterControl;
            _characterControl.currentStats.hp.AsObservable().Subscribe(x => characterHealth.text = x.ToString());

            if (greenBar && yellowBar) {
                _characterControl.currentStats.hp.AsObservable().Pairwise().Subscribe(x =>
                    Utils.UpdateGameObjectLerp(x, greenBar, 2, LerpTime, LerpHealthBar,
                        pair => Utils.UpdateGameObjectLerp(x, yellowBar, 1, LerpTime, LerpHealthBar, _ => { }).AddTo(this)
                    ).AddTo(this)
                ).AddTo(this);
            }

            if (statusEffects) {
                _characterControl.statusEffects.ObserveAdd().Subscribe(async x => {
                    //TODO: Add animation for added effect
                    var added = x.Value;

                    var go = Instantiate(await Utils.LoadResource<GameObject>("StatusEffectIcon"), statusEffects.transform);
                    var image = go.transform.Find("Image").gameObject.GetComponent<Image>();
                    var text = go.transform.Find("TurnsLeft").gameObject.GetComponent<TextMeshProUGUI>();

                    image.sprite = await Utils.LoadResource<Sprite>(added.spriteName);
                    added.turnsLeft.AsObservable().Subscribe(turns => {
                        //TODO: Add animation for text change 
                        text.text = turns.ToString();
                        if (turns == 0) {
                            Destroy(go);
                        }
                    }).AddTo(go);
                }).AddTo(this);
            }
        }

        private static void LerpHealthBar(Pair<float> pair, GameObject go, ref float curLerpTime, float speed, float lerpTime, ref float t) {
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