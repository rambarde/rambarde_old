using UnityEngine;

namespace Characters {
    public class Warrior : Character {
        private void Start() {
            animator = GetComponent<Animator>();
            AnimatorOverrideController myOverrideController = Resources.Load<AnimatorOverrideController>("Warrior");
                        animator.runtimeAnimatorController = myOverrideController;
                    
        }
    }
}