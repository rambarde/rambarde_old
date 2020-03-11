using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Characters;
using TMPro;
using UI;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatManager : MonoBehaviour {
    public List<List<CharacterControl>> teams = new List<List<CharacterControl>>(2);
    public GameObject playerTeamGo, enemyTeamGo;
    public RectTransform playersUiContainer;
    public ReactiveProperty<string> combatPhase = new ReactiveProperty<string>("selectMelody");

    private Canvas _canvas;
    
    public CharacterControl GetTarget(int srcTeam, bool ally) {
        var team = ally ? srcTeam : (srcTeam + 1) % teams.Count;
        return teams[team][(int) (Random.Range(0f, 100f) / 50f) % teams[team].Count];
    }

    public async Task ExecTurn() {
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
        Destroy(characterControl.gameObject);
        
        if (teams[charTeam].Count == 0) Debug.Break();
    }

    #region Unity

    private static CombatManager _instance;
    public static CombatManager Instance => _instance;

    public void Awake() {
        _instance = this;
    }

    private void Start()
    {
        _canvas = playersUiContainer.parent.gameObject.GetComponent<Canvas>();
        
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
        foreach (Transform t in playerTeamGo.transform)
        {
            SetupCharacterControl(t, playerTeam, i, Team.PlayerTeam);
            ++i;
        }

        i = 0;
        foreach (Transform t in enemyTeamGo.transform) {
            SetupCharacterControl(t, enemyTeam, i, Team.EmemyTeam);
            ++i;
        }
    }

    private void SetupCharacterControl(Transform t, IReadOnlyList<CharacterData> team, int i, Team charTeam)
    {
        // instantiate the character prefab
        var go = Instantiate(Utils.LoadResourceFromDir<GameObject>("", "CharacterPrefab"), t);

        // Load the character 3d model
        var model = Instantiate(Utils.LoadResourceFromDir<GameObject>("Models", team[i].modelName), go.transform);
        model.AddComponent<Animator>().runtimeAnimatorController = Utils.LoadResourceFromDir<RuntimeAnimatorController>("", "Character");

        // Init the character control
        var character = go.GetComponent<CharacterControl>();
        character.Init(team[i]);
        character.team = charTeam;
        teams[(int) charTeam].Add(character);

        // instantiate the health bar on the canvas relatively to player position
        var healthBarUi = go.transform.Find("HealthBar");
        var position = t.position;
        Transform hpTransform = healthBarUi.transform;
        hpTransform.parent = playersUiContainer;
        hpTransform.position = Utils.WorldToUiSpace(_canvas, position + Vector3.up * 3.5f);
        hpTransform.localScale = Vector3.one;
        hpTransform.localEulerAngles = Vector3.zero;
        go.GetComponent<CharacterVfx>().Init(character);
        
        // instantiate the skills slot on the canvas relatively to player position
        var slotUiGo = go.transform.Find("SkillSlot");
        var slotsTransform = slotUiGo.transform;
        slotsTransform.parent = playersUiContainer;
        slotsTransform.position = Utils.WorldToUiSpace(_canvas, position + Vector3.down);
        slotsTransform.localScale = Vector3.one;
        slotsTransform.localEulerAngles = Vector3.zero;
        SlotUi slotUi = slotUiGo.GetComponent<SlotUi>();
        slotUi.Init(character);
    }

    #endregion
}