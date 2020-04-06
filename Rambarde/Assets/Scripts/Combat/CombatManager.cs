using System.Collections.Generic;
using System.Threading.Tasks;
using Characters;
using UniRx;
using UnityEngine;

public class CombatManager : MonoBehaviour {
    public List<List<CharacterControl>> teams = new List<List<CharacterControl>>(2);
    public GameObject playerTeamGo, enemyTeamGo;

    public ReactiveProperty<string> combatPhase = new ReactiveProperty<string>("selectMelody");

    private List<Client> clientsMenu;
    private List<ExpeditionMenu.Monster> currentMonsters;

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

        teams[charTeam].Remove(characterControl);
        Destroy(characterControl.gameObject);

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
            //Debug.Break();
        }
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
        var goblin = Utils.LoadResourceFromDir<CharacterData>(dir + "/ForestMonster", "Goblin");
        var goblin1 = Utils.LoadResourceFromDir<CharacterData>(dir + "/ForestMonster", "Goblin");
        var goblin2 = Utils.LoadResourceFromDir<CharacterData>(dir + "/ForestMonster", "Goblin");

        clientsMenu = GameManager.clients;
        currentMonsters = GameManager.quest.fightManager.fights[GameManager.CurrentFight].monsters;

        CharacterData[] playerTeam = {mage, warrior, warrior1};
        CharacterData[] enemyTeam = {goblin, goblin1, goblin2};
        
        teams = new List<List<CharacterControl>> {new List<CharacterControl>(), new List<CharacterControl>()};

        var i = 0;
        foreach (Transform t in playerTeamGo.transform) {
            var go = Instantiate(Utils.LoadResourceFromDir<GameObject>("", "CharacterPrefab"), t);
            go.transform.Find("CharacterCanvas").transform.localEulerAngles = new Vector3(0, -90, 0);
            go.transform.Find("SkillWheel").transform.localEulerAngles = new Vector3(0, -90, 0);
            //var model = Instantiate(Utils.LoadResourceFromDir<GameObject>("Models", playerTeam[i].modelName), go.transform);
            var model = Instantiate(Utils.LoadResourceFromDir<GameObject>("Models", clientsMenu[i].Character.modelName), go.transform);
            model.AddComponent<Animator>().runtimeAnimatorController = Utils.LoadResourceFromDir<RuntimeAnimatorController>("", "Character");
            var character = go.GetComponent<CharacterControl>();
            //character.Init(playerTeam[i]);
            //character.Init(clientsMenu[i]);
            character.Init(clientsMenu[i].Character, clientsMenu[i].SkillWheel);
            character.team = Team.PlayerTeam;
            character.clientNumber = i;
            teams[0].Add(character);
            ++i;
        }

        i = 0;
        foreach (Transform t in enemyTeamGo.transform) {
            var go = Instantiate(Utils.LoadResourceFromDir<GameObject>("", "CharacterPrefab"), t);
            go.transform.Find("CharacterCanvas").transform.localEulerAngles = new Vector3(0, 90, 0);
            go.transform.Find("SkillWheel").transform.localEulerAngles = new Vector3(0, 90, 0);
            //var model = Instantiate(Utils.LoadResourceFromDir<GameObject>("Models", enemyTeam[i].modelName), go.transform);
            var model = Instantiate(Utils.LoadResourceFromDir<GameObject>("Models", currentMonsters[i].Character.modelName), go.transform);
            model.AddComponent<Animator>().runtimeAnimatorController = Utils.LoadResourceFromDir<RuntimeAnimatorController>("", "Character");
            var character = go.GetComponent<CharacterControl>();
            character.team = Team.EmemyTeam;
            //character.Init(enemyTeam[i]);
            //character.Init(currentMonsters[i]);
            character.Init(currentMonsters[i].Character, currentMonsters[i].SkillWheel);
            teams[1].Add(character);
            ++i;
        }
    }

    #endregion
}