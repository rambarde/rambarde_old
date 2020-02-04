using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour {
    public Character[] playerTeam, enemyTeam;

    private Character[][] teams = new Character[2][];

    #region GetRandomChar
    private Character GetRandomChar(int teamNumber) {
        return teams[teamNumber][(int) Random.value % teams[teamNumber].Length];
    }

    public Character GetRandomAlly(int teamNumber) {
        return GetRandomChar(teamNumber);
    }

    public Character GetRandomEnemy(int teamNumber) {
        return GetRandomChar((teamNumber + 1) % teams.Length);
    }
    #endregion

    void Start() {
        teams[0] = playerTeam;
        teams[1] = enemyTeam;
    }

    void Update() { }

    public void ExecTurn() {
        foreach (var team in teams) {
            Debug.Log(team);
            foreach (var character in team) {
                character.ExecuteSkill();
            } 
        }
    }
}