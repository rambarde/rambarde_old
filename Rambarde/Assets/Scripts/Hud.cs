using System.Collections.Generic;
using System.Linq;
using Bard;
using Characters;
using TMPro;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour {
    public List<RectTransform> instPanels;
    public RectTransform actionPanel;
    public RectTransform inspiJauge;

    public void Init(Bard.Bard bard) {
        GameObject buttonPrefab = Utils.LoadResourceFromDir<GameObject>("", "Button");
        GameObject separatorPrefab = Utils.LoadResourceFromDir<GameObject>("", "Separator");

        for (int i = 0; i < bard.instruments.Count; ++i) {
            
            RectTransform panel = instPanels[i];
            Instrument inst = bard.instruments[i];

            int currentTier = 1;
            foreach (var melody in inst.melodies) {
                if (melody.tier > currentTier) {
                    Instantiate(separatorPrefab, panel);
                    currentTier = melody.tier;
                }
                
                GameObject buttonGo = Instantiate(buttonPrefab, panel);
                Button button = buttonGo.GetComponent<Button>();
                TextMeshProUGUI label = buttonGo.GetComponentInChildren<TextMeshProUGUI>();
                label.text = melody.name;
                button.OnClickAsObservable()
                    .Subscribe(_ => {
                        //add listener to character clicks
                        /*foreach (CharacterControl character in CombatManager.Instance.teams[0]) {
                            character.gameObject.OnMouseEnterAsObservable()
                                .Subscribe(__ => Debug.Log(character.gameObject.name));
                        }*/
                        
                        bard.SelectMelody(melody);
                    })
                    .AddTo(button);

                melody.isPlayable.Subscribe(x => { button.interactable = x; }).AddTo(button);
                
                button.OnPointerEnterAsObservable()
                    .Subscribe(_ => { })
                    .AddTo(button);
            }

            bard.actionPoints.Subscribe(p => {
                int nbrSquare = (bard.maxActionPoints.Value - p) / 8; //- - - - - WARNING MAGIC NUMBER NBR NOTE PER BEAT
                int colored = 0;
                List<Image> actionImages = actionPanel.GetComponentsInChildren<Image>().ToList();
                actionImages.RemoveAt(0);
                actionImages.Reverse();
                foreach (Image image in actionImages) {
                    image.color = colored >= nbrSquare ? Color.white : Color.red;
                    colored++; 
                }
            });

            bard.inspiration.current.Subscribe(inspi => {
                inspiJauge.localScale = new Vector3(1, inspi / (float) bard.inspiration.maximumValue, 1);
            });
        }
    }
}