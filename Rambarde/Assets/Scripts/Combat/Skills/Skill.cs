using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bard;
using Characters;
using Status;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Skills {
    [CreateAssetMenu(fileName = "Skill", menuName = "Skill")]
    public class Skill : ScriptableObject {

        public int tier;
        public SkillAction[] actions;
        public string animationName;
        public Sprite sprite;
        [TextArea] public string description;

        private CharacterControl _randEnemy;
        private CharacterControl _randAlly;

        public async Task Execute(CharacterControl s) {
            _randAlly = RandomTargetInTeam(s.team);
            _randEnemy = RandomTargetInTeam(s.team + 1);
            
            foreach (SkillAction action in actions) {
                List<CharacterControl> targets = new List<CharacterControl>();
                switch (action.targetMode) {
                    case SkillTargetMode.OneAlly:
                        targets.Add(_randAlly);
                        break;
                    case SkillTargetMode.OneEnemy:
                        targets.Add(_randEnemy);
                        break;
                    case SkillTargetMode.OneOtherAlly:
                        targets.Add(RandomOtherAlly(s));
                        break;
                    case SkillTargetMode.EveryOtherAlly:
                        targets = new List<CharacterControl>(CombatManager.Instance.teams[(int)s.team]);
                        targets.Remove(s);
                        break;
                        
                    case SkillTargetMode.Self:
                        targets.Add(s);
                        break;
                    case SkillTargetMode.EveryAlly:
                        targets = new List<CharacterControl>(CombatManager.Instance.teams[(int)s.team]);
                        break;
                    case SkillTargetMode.EveryEnemy:
                        targets = new List<CharacterControl>(CombatManager.Instance.teams[(int)(s.team + 1) % 2]);
                        break;
                    case SkillTargetMode.Everyone:
                        targets = new List<CharacterControl>(
                            new List<CharacterControl>(CombatManager.Instance.teams[(int)s.team])
                                .Union(new List<CharacterControl>(CombatManager.Instance.teams[(int)(s.team + 1) % 2])));
                        break;
                    
                    default:
                        Debug.LogError("Tried to execute melody with unknown targetMode [" + action.targetMode + "]");
                        break;
                }

                switch (action.actionType) {
                    case SkillActionType.Attack :
                        targets.ForEach(async t => await t.TakeDamage(action.value / 100f * s.currentStats.atq));
                        break;
                    case SkillActionType.Heal :
                        targets.ForEach(async t => await t.Heal(action.value));
                        break;
                    case SkillActionType.StealHealth :
                        targets.ForEach(async t => {
                            await t.TakeDamage(action.value / 100f * s.currentStats.atq);
                            await s.Heal(action.value / 100f * s.currentStats.atq);
                        });
                        break;
                    case SkillActionType.ApplyEffect :
                        targets.ForEach(async t => await StatusEffect.ApplyEffect(t, action.effectType, (int) action.value));
                        break;
                    case SkillActionType.ApplyBuff :
                        targets.ForEach(async t => await StatusEffect.ApplyBuff(t, action.buffType, (int) action.value));
                        break;
                    case SkillActionType.RemoveEveryEffect :
                        targets.ForEach(t => t.statusEffects.ToList().ForEach(async e => await e.RemoveEffect()));
                        break;
                    case SkillActionType.RemoveEffect :
                        targets.ForEach(async t => await t.statusEffects.First(e => e.type == action.effectType).RemoveEffect());
                        break;
                    case SkillActionType.ShuffleSkillWheel :
                        targets.ForEach(async t => await t.ShuffleSkillsSlot());
                        break;

                    default:
                        Debug.LogError("Tried to execute melody with unknown actionType [" + action.actionType + "]");
                        break;
                }
            }
        }

        private CharacterControl RandomTargetInTeam(Team team) {
            team = (Team) ((int) team % 2);
            float nMemb = CombatManager.Instance.teams[(int) team].Count;
            return CombatManager.Instance.teams[(int)team][Mathf.FloorToInt(Random.Range(0f, nMemb))];
        }
        
        private CharacterControl RandomOtherAlly(CharacterControl s) {
            float nMemb = CombatManager.Instance.teams[(int) s.team].Count;
            List<CharacterControl> team = new List<CharacterControl>(CombatManager.Instance.teams[(int)s.team]);
            team.Remove(s);
            return team[Mathf.FloorToInt(Random.Range(0f, nMemb-1))];
        }
    }
}