using UnityEngine;

namespace Characters {
    public class Mage : Character {
        private void Start() {
            animator = GetComponent<Animator>();
            AnimatorOverrideController myOverrideController = Resources.Load<AnimatorOverrideController>("Mage");
            animator.runtimeAnimatorController = myOverrideController;
            Debug.Log(animator);
        }
    }
}