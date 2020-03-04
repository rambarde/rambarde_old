using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bard;
using Characters;
using Status;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Skills {
    [CreateAssetMenu(fileName = "Skill", menuName = "Skill")]
    public class Skill : ScriptableObject {

        public SkillAction[] actions;
        public string animationName;
        public Sprite sprite;
        [TextArea] public string description;

        public async Task Execute(CharacterControl s) {
            foreach (SkillAction action in actions) {
                List<CharacterControl> targets = new List<CharacterControl>();
                switch (action.targetMode) {
                    case SkillTargetMode.OneAlly:
                        targets.Add(RandomTargetInTeam(s.team));
                        break;
                    case SkillTargetMode.OneEnemy:
                        targets.Add(RandomTargetInTeam(s.team + 1));
                        break;
                    case SkillTargetMode.Self:
                        targets.Add(s);
                        break;
                    case SkillTargetMode.EveryAlly :
                        targets = new List<CharacterControl>(CombatManager.Instance.teams[(int)s.team]);
                        break;
                    case SkillTargetMode.EveryEnemy :
                        targets = new List<CharacterControl>(CombatManager.Instance.teams[(int)s.team + 1]);
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
                    case SkillActionType.ApplyEffect :
                        targets.ForEach(async t => await StatusEffect.ApplyEffect(t, action.effectType, (int) action.value));
                        break;
                    case SkillActionType.ApplyBuff :
                        targets.ForEach(async t => await StatusEffect.ApplyBuff(t, action.buffType, (int) action.value));
                        break;

                    default:
                        Debug.LogError("Tried to execute melody with unknown actionType [" + action.actionType + "]");
                        break;
                }
            }
        }

        private CharacterControl RandomTargetInTeam(Team team) {
            team = (Team) ((int) team % 2);
            return CombatManager.Instance.teams[(int)team][Mathf.FloorToInt(Random.Range(0f, 2f))];
        }
    }
}