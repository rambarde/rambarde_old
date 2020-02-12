using System.Collections.Generic;
using System.Linq;
using Unity.UIElements.Runtime;
using UnityEngine;
using UnityEngine.UIElements;

public class Hud : MonoBehaviour {
    [SerializeField] private Bard _bard;

    private PanelRenderer _panelRenderer;

    private void Awake() {
        _panelRenderer = GetComponent<PanelRenderer>();
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

        return null;
    }
}