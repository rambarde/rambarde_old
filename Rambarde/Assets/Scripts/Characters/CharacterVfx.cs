using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace Characters {
    public class CharacterVfx : MonoBehaviour {
        [SerializeField] private Character character;
        [SerializeField] private GameObject greenBar;
        [SerializeField] private GameObject yellowBar;
        [SerializeField] private GameObject statusEffects;

        private const float LerpTime = 1f;
        private const string ResourcesDir = "CharacterVfx";

        private TextMeshProUGUI _characterHealth;

        private void Start() {
            Debug.Log(character.name);
            _characterHealth = GetComponentInChildren<TextMeshProUGUI>();
            if (_characterHealth == null) return;

            character.stats.hp.AsObservable().Subscribe(x => _characterHealth.text = x.ToString());

            if (greenBar && yellowBar) {
                character.stats.hp.AsObservable().Pairwise().Subscribe(x =>
                    Utils.UpdateGameObjectLerp(x, greenBar, 2, LerpTime, LerpHealthBar,
                        pair => Utils.UpdateGameObjectLerp(x, yellowBar, 1, LerpTime, LerpHealthBar, _ => { }).AddTo(this)
                    ).AddTo(this)
                ).AddTo(this);
            }

            if (statusEffects) {
                character.statusEffects.ObserveAdd().Subscribe(x => {
                    // TODO: Add animation for added effect
                    var added = x.Value;

                    var go = Instantiate(Utils.LoadResourceFromDir<GameObject>(ResourcesDir, "StatusEffectIcon"), statusEffects.transform);
                    var image = go.transform.Find("Image").gameObject.GetComponent<Image>();
                    var text = go.transform.Find("TurnsLeft").gameObject.GetComponent<TextMeshProUGUI>();

                    image.sprite = Utils.LoadResourceFromDir<Sprite>(ResourcesDir, added.spriteName);
                    added.turnsLeft.AsObservable().Subscribe(turns => {
                        // TODO: Add animation for text change 
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