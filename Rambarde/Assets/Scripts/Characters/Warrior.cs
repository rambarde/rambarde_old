using UnityEngine;

namespace Characters {
    public class Warrior : Character {
        private void Start() {
            Animator = GetComponent<Animator>();
            AnimatorOverrideController myOverrideController = Resources.Load<AnimatorOverrideController>("Warrior");
                        Animator.runtimeAnimatorController = myOverrideController;
                    
        }
    }
}