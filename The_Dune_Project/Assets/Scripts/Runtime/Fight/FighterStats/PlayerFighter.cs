using UnityEngine;

namespace Fight
{
    public class PlayerFighter : Fighter
    {
        [Header("Animator")] [SerializeField] public AnimatorManager animatorManager;

        protected override void Init()
        {
            base.Init();

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