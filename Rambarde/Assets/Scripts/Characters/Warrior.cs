using UnityEngine;

namespace Characters {
    public class Warrior : Character {
        protected new void Start() {
            base.Start();
            CombatManager = CombatManager.Instance;
            Animator = GetComponent<Animator>();
            AnimatorOverrideController myOverrideController = Resources.Load<AnimatorOverrideController>("Warrior");
                        Animator.runtimeAnimatorController = myOverrideController;
                    
        }
    }
}