using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Characters;
using UnityEngine;

public class CombatManager : MonoBehaviour {
    public Character[] playerTeam, enemyTeam;

    private List<List<Character>> teams = new List<List<Character>>(2);

    public void Remove(Character character) {
        var charTeam = (int) character.team;
        teams[charTeam].Remove(character);
        if (teams[charTeam].Count == 0) Debug.Break();
        Destroy(character.gameObject);
    }

    #region GetRandomChar

    private Character GetRandomChar(int teamNumber) {
        return teams[teamNumber][(int) Random.value % teams[teamNumber].Count];
    }

    public Character GetRandomAlly(int teamNumber) {
        return GetRandomChar(teamNumber);
    }

    public Character GetRandomEnemy(int teamNumber) {
        return GetRandomChar((teamNumber + 1) % teams.Count);
    }

    #endregion

    public void ExecuteTurn() {
        StartCoroutine(nameof(ExecTurn));
    }

    private async void ExecTurn() {
        // Apply status effects to all characters
        foreach (var team in teams) {
            foreach (var character in team) {
                var l = character.transform.Find("HighLight").gameObject;
                l.SetActive(true);
                Debug.Log(l);
                foreach (var effect in character.StatusEffects) {
                    // Debug.Log(effect);
                    // yield return StartCoroutine(effect.TurnStart());
                    await effect.TurnStart();
                }

                Debug.Log("VARasd");
                l.SetActive(false);
            }
        }
        
        // Execute all character skills
        foreach (var team in teams) {
            foreach (var character in team) {
                var l = character.transform.Find("HighLight").gameObject;
                l.SetActive(true);
                await character.ExecuteSkill();
                l.SetActive(false);
            }
        }
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