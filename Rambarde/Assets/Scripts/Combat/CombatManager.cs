
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Characters;
using Combat.Characters;
using TMPro;
using UI;
using UniRx;
using UnityEngine;
using Random = UnityEngine.Random;

public class CombatManager : MonoBehaviour {
    public List<List<CharacterControl>> teams = new List<List<CharacterControl>>(2);
    public GameObject playerTeamGo, enemyTeamGo;
    public RectTransform playerTeamUiContainer;
    public RectTransform enemyTeamUiContainer;
    public ReactiveProperty<string> combatPhase = new ReactiveProperty<string>("selectMelody");
    
    private List<CharacterBase> clientsMenu;
    private List<CharacterBase> currentMonsters;
    //private Canvas _canvas;
    
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
        if(charTeam == 0)
            GameManager.quest.FightMax[characterControl.clientNumber] = GameManager.CurrentFight + 1; //decreasing the number of total fight

        Destroy(characterControl.gameObject);
        teams[charTeam].Remove(characterControl);

        if (teams[charTeam].Count == 0)
        { 
            GetComponent<GameManager>().ChangeCombat();
            if (!GameManager.QuestState)
                GetComponent<GameManager>().ChangeScene(2);
            else
            {
                var gold = GetComponent<GameManager>().CalculateGold();
                GetComponent<GameManager>().ChangeScene(0);
            }
        }
    }

    #region Unity

    private static CombatManager _instance;
    public static CombatManager Instance => _instance;

    public void Awake() {
        _instance = this;
    }

    private async void Start()
    {
        clientsMenu = new List<CharacterBase>();
        foreach (var client in GameManager.clients)
            clientsMenu.Add(client);

        currentMonsters = new List<CharacterBase>();
        foreach (var monster in GameManager.quest.fightManager.fights[GameManager.CurrentFight].monsters)
            clientsMenu.Add(monster);
            ;
     
        teams = new List<List<CharacterControl>> {new List<CharacterControl>(), new List<CharacterControl>()};

        var i = 0;
        foreach (Transform t in playerTeamGo.transform)
        {
            SetupCharacterControl(t, clientsMenu, i, Team.PlayerTeam);
            ++i;
        }

        i = 0;
        foreach (Transform t in enemyTeamGo.transform) {
            SetupCharacterControl(t, currentMonsters, i, Team.EmemyTeam);
            ++i;
        }
    }

    private async void SetupCharacterControl(Transform characterTransform, IReadOnlyList<CharacterBase> team, int i, Team charTeam)
    {
        string charPrefabName = charTeam == Team.PlayerTeam ? "PlayerTeamCharacterPrefab" : "EnemyCharacterPrefab";
        string charPrefabUIName = charTeam == Team.PlayerTeam ? "PlayerTeamCharacterUI" : "EnemyCharacterUI";

        // instantiate the character prefab
        var characterGameObject = Instantiate(await Utils.LoadResource<GameObject>(charPrefabName), characterTransform);

        // Load the character 3d model
        var model = Instantiate(await Utils.LoadResource<GameObject>(team[i].Character.modelName), characterGameObject.transform);
        model.AddComponent<Animator>().runtimeAnimatorController = await Utils.LoadResource<RuntimeAnimatorController>("Animations/Character");

        // Init the character control
        var character = characterGameObject.GetComponent<CharacterControl>();
        character.Init(team[i].Character, team[i].SkillWheel);
        character.team = charTeam;
        teams[(int) charTeam].Add(character);

        // instantiate the UI on the canvas
        var charUi = characterGameObject.transform.Find(charPrefabUIName);
        charUi.SetParent(charTeam == Team.PlayerTeam ? playerTeamUiContainer : enemyTeamUiContainer);
        charUi.localScale = Vector3.one;
        charUi.localEulerAngles = Vector3.zero;
        characterGameObject.GetComponent<CharacterVfx>().Init(character);
        SlotUi slotUi = charUi.GetComponentInChildren<SlotUi>();
        slotUi.Init(character);
    }

    #endregion
}
