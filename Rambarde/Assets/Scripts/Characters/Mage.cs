using Structs;
using UnityEngine;

namespace Characters {
    public class Mage : Character {
        protected new void Start() {
            base.Start();
            Animator = GetComponent<Animator>();
            AnimatorOverrideController myOverrideController = Resources.Load<AnimatorOverrideController>("Mage");
            Animator.runtimeAnimatorController = myOverrideController;
        }
    }
}