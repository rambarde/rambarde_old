using System.Collections.Generic;
using System.Linq;
using Characters;
using UnityEngine;

public class CombatManager : MonoBehaviour {
    public Character[] playerTeam, enemyTeam;

    public List<List<Character>> teams = new List<List<Character>>(2);

    public Character GetRandomChar(int srcTeam, bool ally) {
        Character c;
        if (ally) {
            c = teams[srcTeam][(int) Random.value % teams[srcTeam].Count];
            return c;
        }

        var team = (srcTeam + 1) % teams.Count;
        c = teams[team][(int) Random.value % teams[team].Count];
        return c;
    }

    public async void ExecTurn() {
        // Apply status effects to all characters
        foreach (var team in teams) {
            for (var index = team.Count - 1; index >= 0; --index) {
                var character = team[index];

                var l = character.transform.Find("HighLight").gameObject;
                l.SetActive(true);

                await character.EffectsTurnStart();

                l.SetActive(false);
            }
        }

        // Execute all character skills
        foreach (var team in teams) {
            for (var i = team.Count - 1; i >= 0; --i) {
                var character = team[i];
                var l = character.transform.Find("HighLight").gameObject;
                l.SetActive(true);

                await character.ExecTurn();

                l.SetActive(false);
            }
        }
    }

    public void Remove(Character character) {
        var charTeam = (int) character.team;
        teams[charTeam].Remove(character);
        if (teams[charTeam].Count == 0) Debug.Break();
        Destroy(character.gameObject);
    }

    #region Unity

    private static CombatManager _instance;
    public static CombatManager Instance => _instance;

    public void Awake() {
        _instance = this;
    }

    void Start() {
        teams.Add(playerTeam.ToList());
        teams.Add(enemyTeam.ToList());
    }

    #endregion
}