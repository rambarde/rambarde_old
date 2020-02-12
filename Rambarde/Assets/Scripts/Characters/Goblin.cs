using UnityEngine;

namespace Characters {
    public class Goblin : Character {
        private void Start() {
            CombatManager = CombatManager.Instance;
            Animator = GetComponent<Animator>();
            AnimatorOverrideController myOverrideController = Resources.Load<AnimatorOverrideController>("Goblin");
            Animator.runtimeAnimatorController = myOverrideController;
            // Debug.Log(animator);
        }
    }
}