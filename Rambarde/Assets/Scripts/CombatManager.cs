using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

    public void ExecTurn() {
        foreach (var team in teams) {
            // Apply status effects to all characters
            foreach (var character in team) {
                foreach (var effect in character.StatusEffects) {
                    Debug.Log(effect);
                    effect.TurnStart();
                }
            }

            // Execute all character skills
            foreach (var character in team) {
                character.ExecuteSkill();
            }
        }
    }

    #region Unity

    void Start() {
        teams[0] = playerTeam;
        teams[1] = enemyTeam;
    }

    #endregion
}