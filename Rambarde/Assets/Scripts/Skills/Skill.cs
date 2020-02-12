﻿using Characters;
using Structs;
using UnityEngine;

namespace Skills {
    public abstract class Skill : ScriptableObject {
        [SerializeField] private bool shouldCastOnAllies;

        public bool ShouldCastOnAllies => shouldCastOnAllies;

        public abstract void Execute(Stats source, Character target);

        public string skillName;

        public Sprite sprite;

        public AnimationClip animation;
    }
}