using Music;
using UniRx;
using UnityEngine;

public class MusicPlanner : MonoBehaviour {
    [SerializeField] private string[] melodies;
    [SerializeField] private int maxEnergy;

    [SerializeField] private MusicSheet musicSheet;
    // [SerializeField] private Text partition;

    private int _energy;
    public ReactiveProperty<int> usedEnergy;
    private string _partitionToPlay;

    public MusicPlanner(int energy) {
        _energy = energy;
    }

    public void PlaceMelody(int index) {
        var s = melodies[index];
        var newEnergy = s.Length / musicSheet.nbrBeat + usedEnergy.Value;
        if (newEnergy > _energy) {
            return;
        }

        for (int i = 0; i < s.Length; ++i) {
            if (s[i] != '-') {
                musicSheet.PlaceNote(i + _partitionToPlay.Length, s[i] - '0');
            }
        }

        SetPartition(_partitionToPlay + s);
        usedEnergy.Value = newEnergy;
        Debug.Log(_partitionToPlay);
    }

    public void Done() {
        _energy = maxEnergy + maxEnergy - usedEnergy.Value;
        musicSheet.StartPlaying(new MelodyData(_partitionToPlay));
        CombatManager.Instance.ExecTurn();
        // Debug.Log(CombatManager.Instance.name);

        Reset();
    }

    public void Reset() {
        SetPartition("");
        usedEnergy.Value = 0;
    }

    private void SetPartition(string p) {
        _partitionToPlay = p;
        // partition.text = p;
    }

    void Start() {
        SetPartition("");
        _energy = maxEnergy;
        usedEnergy = new ReactiveProperty<int>(0);
    }
}