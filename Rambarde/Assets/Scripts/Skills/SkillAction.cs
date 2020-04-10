﻿using Status;

namespace Skills {

    [System.Serializable]
    public struct SkillAction {
        public SkillActionType actionType;
        public SkillTargetMode targetMode;
        public EffectType effectType;
        public BuffType buffType;
        public float value;
    }
    
    public enum SkillActionType {
        Attack,
        Heal,
        ApplyEffect,
        ApplyBuff
    }

    public enum SkillTargetMode {
        OneAlly,
        OneEnemy,
        Self,
        EveryAlly,
        EveryEnemy
    }
}