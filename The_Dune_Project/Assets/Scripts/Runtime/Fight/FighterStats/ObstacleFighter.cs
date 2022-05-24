using System.Collections;
using System.Collections.Generic;
using Fight;
using UnityEngine;

namespace Fight
{
    public class ObstacleFighter : Fighter
    {
        private Fighter fighter;
        
        void Start()
        {
            fighter = GetComponent<Fighter>();
        }

        protected override void KillEntity()
        {
            
        }
        
        public override void TakeDamage(int damage)
        {
            
        }

    }
}
