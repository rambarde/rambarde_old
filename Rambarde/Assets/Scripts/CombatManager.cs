using System.Collections.Generic;
using Characters;
using UnityEngine;

public class CombatManager : MonoBehaviour {
    public List<List<CharacterControl>> teams = new List<List<CharacterControl>>(2);
    public GameObject playerTeamGo, enemyTeamGo;

    public CharacterControl GetTarget(int srcTeam, bool ally) {
        CharacterControl c;
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

    public void Remove(CharacterControl characterControl) {
        var charTeam = (int) characterControl.team;
        teams[charTeam].Remove(characterControl);
        if (teams[charTeam].Count == 0) Debug.Break();
        Destroy(characterControl.gameObject);
    }

    #region Unity

    private static CombatManager _instance;
    public static CombatManager Instance => _instance;

    public void Awake() {
        _instance = this;
    }

    private void Start() {
        const string dir = "ScriptableObjects/Characters";
        var mage = Utils.LoadResourceFromDir<CharacterData>(dir, "Mage");
        var warrior = Utils.LoadResourceFromDir<CharacterData>(dir, "Warrior");
        var warrior1 = Utils.LoadResourceFromDir<CharacterData>(dir, "Warrior");
        var goblin = Utils.LoadResourceFromDir<CharacterData>(dir, "Goblin");
        var goblin1 = Utils.LoadResourceFromDir<CharacterData>(dir, "Goblin");
        var goblin2 = Utils.LoadResourceFromDir<CharacterData>(dir, "Goblin");

        CharacterData[] playerTeam = {mage, warrior, warrior1};
        CharacterData[] enemyTeam = {goblin, goblin1, goblin2};

        teams = new List<List<CharacterControl>> {new List<CharacterControl>(), new List<CharacterControl>()};

        var i = 0;
        foreach (Transform t in playerTeamGo.transform) {
            var go = Instantiate(Utils.LoadResourceFromDir<GameObject>("", "CharacterPrefab"), t);
            var model = Instantiate(Utils.LoadResourceFromDir<GameObject>("Models", playerTeam[i].modelName), go.transform);
            model.AddComponent<Animator>().runtimeAnimatorController = Utils.LoadResourceFromDir<RuntimeAnimatorController>("", "Character");
            var character = go.GetComponent<CharacterControl>();
            character.Init(playerTeam[i]);
            character.team = Team.PlayerTeam;
            teams[0].Add(character);
            ++i;
        }

        i = 0;
        foreach (Transform t in enemyTeamGo.transform) {
            var go = Instantiate(Utils.LoadResourceFromDir<GameObject>("", "CharacterPrefab"), t);
            var model = Instantiate(Utils.LoadResourceFromDir<GameObject>("Models", enemyTeam[i].modelName), go.transform);
            model.AddComponent<Animator>().runtimeAnimatorController = Utils.LoadResourceFromDir<RuntimeAnimatorController>("", "Character");
            var character = go.GetComponent<CharacterControl>();
            character.team = Team.EmemyTeam;
            character.Init(enemyTeam[i]);
            teams[1].Add(character);
            ++i;
        }
    }

    #endregion
}