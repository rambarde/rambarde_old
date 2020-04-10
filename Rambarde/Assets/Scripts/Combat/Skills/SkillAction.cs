using Status;

namespace Skills {

    [System.Serializable]
    public class SkillAction {
        public SkillActionType actionType;
        public SkillTargetMode targetMode;
        public EffectType effectType;
        public BuffType buffType;
        public float value;
    }
    public enum SkillActionType {
        Attack,
        Heal,
        StealHealth,
        ApplyEffect,
        ApplyBuff,
        RemoveEveryEffect,
        RemoveEffect,
        ShuffleSkillWheel
    }

    public enum SkillTargetMode {
        OneAlly,
        OneEnemy,
        Self,
        EveryAlly,
        EveryEnemy,
        OneOtherAlly,
        EveryOtherAlly,
        Everyone
    }
}