using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Bard : MonoBehaviour {
    [SerializeField] private string[] melodies;
    [SerializeField] private int maxEnergy;
    [SerializeField] private Text partition;

    private int _energy;
    private int _usedEnergy;
    private string _partitionToPlay;
    private const int TimeSig = 4;

    public void PlaceMelody(int index) {
        var s = melodies[index];
        var newEnergy = s.Length / TimeSig + _usedEnergy;
        if (newEnergy > _energy) {
            return;
        }
        SetPartition(_partitionToPlay + s);
        _usedEnergy = newEnergy;
    }

    public void Done() {
        CombatManager.Instance.ExecuteTurn();
        _energy = maxEnergy + maxEnergy - _usedEnergy;
        Reset();
        Debug.Log(_partitionToPlay);
        Debug.Log(_energy);
    }

    public void Reset() {
        SetPartition("");
        _usedEnergy = 0;
    }

    private void SetPartition(string p) {
        _partitionToPlay = p;
        partition.text = p;
    }

    void Start() {
        SetPartition("");
        _energy = maxEnergy;
    }
}