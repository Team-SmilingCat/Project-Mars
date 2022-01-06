using UnityEngine;

namespace Fight
{
    public class PlayerFighter : Fighter
    {
        [Header("Animator")] [SerializeField] private AnimatorManager animatorManager;
        [SerializeField] private PlayerUIManager playerUIManager;
        [SerializeField] private int healthScale = 1;

        protected override void Init()
        {
            base.Init();
            
            int maxHealth = stats.maxHealth * healthScale;
            playerUIManager.SetMaxHealth(maxHealth);
        }
        
        protected override void KillEntity()
        {
            base.KillEntity();
            
            Rigidbody rb = GetComponent<Rigidbody>();
            if (rb)
            {
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }

            if (animatorManager)
            {
                animatorManager.PlayTargetAnimation("death", true);
            }
        }
    }
}