using UnityEngine;

namespace Characters {
    public class Goblin : Character {
        protected new void Start() {
            base.Start();
            CombatManager = CombatManager.Instance;
            Animator = GetComponent<Animator>();
            AnimatorOverrideController myOverrideController = Resources.Load<AnimatorOverrideController>("Goblin");
            Animator.runtimeAnimatorController = myOverrideController;
            // Debug.Log(animator);
        }
    }
}