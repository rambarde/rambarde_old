using System;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using Unity.UIElements.Runtime;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

public class Hud : MonoBehaviour {
    [SerializeField] private Bard _bard;

    private PanelRenderer _panelRenderer;
    private UIElementsEventSystem _eventSystem;

    private void Awake() {
        _panelRenderer = GetComponent<PanelRenderer>();
        _eventSystem = GetComponent<UIElementsEventSystem>();
    }

    private void OnEnable() {
        _panelRenderer.postUxmlReload = BindUi;
    }

    private IEnumerable<Object> BindUi() {
        var root = _panelRenderer.visualTree;

        var bs = root.Q<VisualElement>("melodies");

        bs.Children().Select(x => x as Button)
            .Where(x => x != null)
            .ToList()
            .ForEach(x => x.clickable.clicked += () => _bard.PlaceMelody(int.Parse(x.name.Substring(1))));

        var reset = root.Q<Button>("reset");
        if (reset != null) {
            reset.clickable.clicked += _bard.Reset;
        }

        var done = root.Q<Button>("done");
        if (done != null) {
            done.clickable.clicked += _bard.Done;
        }

        var energyText = root.Q<Label>("label");
        if (energyText != null) {
            _bard.usedEnergy.AsObservable().Subscribe(x => energyText.text = x.ToString());
        }
        
        return null;
    }
}