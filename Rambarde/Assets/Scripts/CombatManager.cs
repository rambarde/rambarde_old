using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Characters;
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
        foreach (Transform t in playerTeamGo.transform) {
            // instantiate the character prefab
            var go = Instantiate(Utils.LoadResourceFromDir<GameObject>("", "CharacterPrefab"), t);
            
            // Load the character 3d model
            var model = Instantiate(Utils.LoadResourceFromDir<GameObject>("Models", playerTeam[i].modelName), go.transform);
            model.AddComponent<Animator>().runtimeAnimatorController = Utils.LoadResourceFromDir<RuntimeAnimatorController>("", "Character");
            
            // Init the character control
            var character = go.GetComponent<CharacterControl>();
            character.Init(playerTeam[i]);
            character.team = Team.PlayerTeam;
            teams[0].Add(character);
            
            // instantiate the health bar on the canvas relatively to player position
            var healthBarUi = Instantiate(Utils.LoadResourceFromDir<GameObject>("", "HealthBar"), playersUiContainer);
            healthBarUi.name = "HealthBar" + t.gameObject.name;
            var position = t.position;
            healthBarUi.transform.position = Utils.WorldToUiSpace(_canvas, position + Vector3.up * 3.5f);
            var vfx = go.GetComponent<CharacterVfx>();
            vfx.greenBar = healthBarUi.transform.Find("GreenBar").gameObject;
            vfx.yellowBar = healthBarUi.transform.Find("YellowBar").gameObject;
            vfx.statusEffects = healthBarUi.transform.Find("StatusEffects").gameObject;
            
            // instantiate the skills slot on the canvas relatively to player position
            var slotUiGo = Instantiate(Utils.LoadResourceFromDir<GameObject>("", "SkillSlot"), playersUiContainer);
            slotUiGo.name = "SkillSlot" + t.gameObject.name;
            slotUiGo.transform.position = Utils.WorldToUiSpace(_canvas, position + Vector3.down);
            SlotUi slotUi = go.GetComponent<SlotUi>();
            slotUi.tooltip = slotUiGo.transform.Find("Tooltip").gameObject;
            foreach (RectTransform rectTransform in slotUiGo.transform.Find("Mask").transform)
                slotUi.slotIconPositions.Add(rectTransform);
            slotUi.Init();
            slotUi.indicator = slotUiGo.transform.Find("Indicator").transform as RectTransform;
            ++i;
        }

        i = 0;
        foreach (Transform t in enemyTeamGo.transform) {
            // instantiate the character prefab
            var go = Instantiate(Utils.LoadResourceFromDir<GameObject>("", "CharacterPrefab"), t);
            
            // Load the character 3d model
            var model = Instantiate(Utils.LoadResourceFromDir<GameObject>("Models", enemyTeam[i].modelName), go.transform);
            model.AddComponent<Animator>().runtimeAnimatorController = Utils.LoadResourceFromDir<RuntimeAnimatorController>("", "Character");
            
            // Init the character control
            var character = go.GetComponent<CharacterControl>();
            character.team = Team.EmemyTeam;
            character.Init(enemyTeam[i]);
            teams[1].Add(character);
            
            // instantiate the health bar on the canvas relatively to player position
            var healthBarUi = Instantiate(Utils.LoadResourceFromDir<GameObject>("", "HealthBar"), playersUiContainer);
            healthBarUi.name = "HealthBar" + t.gameObject.name;
            var position = t.position;
            healthBarUi.transform.position = Utils.WorldToUiSpace(_canvas, position + Vector3.up * 3.5f);
            var vfx = go.GetComponent<CharacterVfx>();
            vfx.greenBar = healthBarUi.transform.Find("GreenBar").gameObject;
            vfx.yellowBar = healthBarUi.transform.Find("YellowBar").gameObject;
            vfx.statusEffects = healthBarUi.transform.Find("StatusEffects").gameObject;
            
            // instantiate the skills slot on the canvas relatively to player position
            var slotUiGo = Instantiate(Utils.LoadResourceFromDir<GameObject>("", "SkillSlot"), playersUiContainer);
            slotUiGo.name = "SkillSlot" + t.gameObject.name;
            slotUiGo.transform.position = Utils.WorldToUiSpace(_canvas, position + Vector3.down);
            SlotUi slotUi = go.GetComponent<SlotUi>();
            slotUi.tooltip = slotUiGo.transform.Find("Tooltip").gameObject;
            foreach (RectTransform rectTransform in slotUiGo.transform.Find("Mask").transform)
                slotUi.slotIconPositions.Add(rectTransform);
            slotUi.Init();
            slotUi.indicator = slotUiGo.transform.Find("Indicator").transform as RectTransform;
            ++i;
        }
    }

    #endregion
}