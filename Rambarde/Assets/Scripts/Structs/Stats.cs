using System;
using UnityEditor.Experimental.UIElements.GraphView;
using UnityEngine;

[Serializable]
public struct Stats {
    public Stats(int end, float atq, float prot, float crit) {
        _end = end;
        _atq = atq;
        _prot = prot;
        _crit = crit;
    }

    // Endurance
    [SerializeField] private int _end;
    public int End => _end;


    // Attack 
    [SerializeField] private float _atq;
    public float Atq => _atq;

    // Protection (percentage)
    [SerializeField] private float _prot;
    public float Prot => _prot;

    // Critical (percentage)
    [SerializeField] private float _crit;
    public float Crit => _crit;
}