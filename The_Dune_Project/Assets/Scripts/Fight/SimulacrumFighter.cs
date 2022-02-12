using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Fight{
public class SimulacrumFighter : Fighter
    {
        [SerializeField] Explosion explosion;
        private Fighter fighter;

        private void Start()
        {
            fighter = GetComponent<Fighter>();
        }

        protected override void KillEntity()
        {
            base.KillEntity();
            explosion.explode();

            if (animator)
            {
                animator.Play("death");
            }
        }
    }
}
