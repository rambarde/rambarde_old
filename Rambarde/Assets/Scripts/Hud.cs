using System;
using System.Collections.Generic;
using System.Linq;
using Bard;
using UniRx;
using UniRx.Triggers;
using Unity.UIElements.Runtime;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public List<RectTransform> instPanels;

    public void Init(Bard.Bard bard)
    {
        GameObject buttonPrefab = Utils.LoadResourceFromDir<GameObject>("", "Button");
        GameObject separatorPrefab = Utils.LoadResourceFromDir<GameObject>("", "Separator");

        for (int i = 0; i < bard.instruments.Count; ++i)
        {
            RectTransform panel = instPanels[i];
            Instrument inst = bard.instruments[i];

            int currentTier = 1;
            foreach (var melody in inst.melodies)
            {
                if (melody.tier > currentTier)
                {
                    Instantiate(separatorPrefab, panel);
                    currentTier = melody.tier;
                }
                var buttonGo = Instantiate(buttonPrefab, panel);
                var button = buttonGo.GetComponent<Button>();
                button.OnClickAsObservable()
                    .Subscribe(_ => { bard.SelectMelody(melody); })
                    .AddTo(button);

                melody.isPlayable.Subscribe(x => { button.interactable = x; }).AddTo(button);
                
                button.OnPointerEnterAsObservable()
                    .Subscribe(_ => { })
                    .AddTo(button);
            }
        }
    }
}