using UnityEngine;

namespace Characters {
    public class Mage : Character {
        protected new void Start() {
            base.Start();
            CombatManager = CombatManager.Instance;
            Animator = GetComponent<Animator>();
            AnimatorOverrideController myOverrideController = Resources.Load<AnimatorOverrideController>("Mage");
            Animator.runtimeAnimatorController = myOverrideController;
            // Debug.Log(animator);
        }
    }
}