using Music;
using Status;
using Unity.UIElements.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class Bard : MonoBehaviour {
    [SerializeField] private string[] melodies;
    [SerializeField] private int maxEnergy;
    [SerializeField] private MusicSheet musicSheet;
    // [SerializeField] private Text partition;

    private int _energy;
    private int _usedEnergy;
    private string _partitionToPlay;

    public void PlaceMelody(int index) {
        var s = melodies[index];
        var newEnergy = s.Length / musicSheet.nbrBeat + _usedEnergy;
        if (newEnergy > _energy) {
            return;
        }

        SetPartition(_partitionToPlay + s);
        _usedEnergy = newEnergy;
        Debug.Log(_partitionToPlay);
    }

    public void Done() {
        _energy = maxEnergy + maxEnergy - _usedEnergy;
        musicSheet.StartPlaying(new MelodyData(_partitionToPlay));
        CombatManager.Instance.ExecuteTurn();
        Debug.Log(CombatManager.Instance.name);
        
        Reset();
    }

    public void Reset() {
        SetPartition("");
        _usedEnergy = 0;
    }

    private void SetPartition(string p) {
        _partitionToPlay = p;
        // partition.text = p;
    }

    void Start() {
        SetPartition("");
        _energy = maxEnergy;
    }
}