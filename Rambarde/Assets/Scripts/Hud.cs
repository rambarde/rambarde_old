using System;
using System.Collections.Generic;
using System.Linq;
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

        for (int i = 0; i < bard.instruments.Count; ++i)
        {
            var panel = instPanels[i];
            var inst = bard.instruments[i];

            foreach (var melody in inst.melodies)
            {
                var buttonGo = Instantiate(buttonPrefab, panel);
                var button = buttonGo.GetComponent<Button>();
                button.OnClickAsObservable()
                    .Subscribe(_ => { bard.SelectMelody(melody); });

                melody.isPlayable.Subscribe(x => { button.interactable = x; });

                button.OnPointerEnterAsObservable()
                    .Subscribe(_ => { Debug.Log("Pointer is, in fact, entering this thang.");});
            }
        }
    }
}