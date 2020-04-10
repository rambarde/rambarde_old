using System;
using System.Linq;
using System.Threading.Tasks;
using Bard;
using Characters;
using JetBrains.Annotations;
using Status;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Melodies {
    public abstract class Melody : ScriptableObject {
        [SerializeField] private string data;
        public string Data => data;
        public int Size => data.Length; 
        
        public MelodyTargetMode targetMode;
        [NonSerialized] public ReactiveProperty<int> score = new ReactiveProperty<int>(0);
        
        [NonSerialized] public CharacterControl target = null;
        [NonSerialized] public ReactiveProperty<bool> isPlayable = new ReactiveProperty<bool>(true);

        public int tier;
        [TextArea] public string effect;
        public int inspirationValue;
        public int tranceValue;                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                
        public Sprite sprite;
        public Color color = new Color(1, 1, 1, 1);

        public async Task Execute() {
            switch (targetMode) {
                case MelodyTargetMode.Anyone :
                case MelodyTargetMode.OneAlly :
                case MelodyTargetMode.OneEnemy :
                    if (target.HasEffect(EffectType.Deaf)) {
                        break;
                    }
                    await ExecuteOnTarget(target);
                    break;
                case MelodyTargetMode.Everyone :
                    foreach (CharacterControl character in CombatManager.Instance.teams
                            .SelectMany(_=>_)
                            .Where(c => !c.HasEffect(EffectType.Deaf))) {
                        await ExecuteOnTarget(character);
                    }
                    break;
                case MelodyTargetMode.EveryAlly :
                    foreach (CharacterControl character in CombatManager.Instance.teams[0]
                            .Where(c => !c.HasEffect(EffectType.Deaf))) {
                        await ExecuteOnTarget(character);
                    }
                    break;
                case MelodyTargetMode.EveryEnemy :
                    foreach (CharacterControl character in CombatManager.Instance.teams[1]
                            .Where(c => !c.HasEffect(EffectType.Deaf))) {
                        await ExecuteOnTarget(character);
                    }
                    break;
            }

        }

        protected abstract Task ExecuteOnTarget(CharacterControl t);

    }
}