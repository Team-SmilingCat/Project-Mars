using System;
using UnityEngine;

namespace Fight
{
    public class ImperiumFighter : Fighter
    {
        private Fighter fighter;
        
        private void Start()
        {
            fighter = GetComponent<Fighter>();
        }

        protected override void KillEntity()
        {
            base.KillEntity();
            
            if (animator)
            {
                animator.Play("death");
            }
        }
        
        void LateUpdate()
        {
            Transform target = GameObject.FindWithTag("Player").transform;

            if (fighter && (transform.position.x - target.transform.position.x < 50) && !fighter.inCombat && 
                fighter.curHealth > 0)
            {
                animator.Play("shoot");
            }
        
        }
    }
}