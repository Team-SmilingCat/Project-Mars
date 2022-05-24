using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Fight{
public class SimulacrumFighter : Fighter
    {
        protected override void KillEntity()
        {
            base.KillEntity();
            if (animator)
            {
                animator.Play("death");
            }
        }
        
    }
}
