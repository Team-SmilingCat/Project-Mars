using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private AnimatorManager animatorManager;
    [SerializeField] private PlayerInputHandle playerInputHandle;
    
    [Header("attack settings")] private string prevAtk;
    
    public void handleAttack(Weapons weapon)
    {
        animatorManager.PlayTargetAnimation(weapon.atk1, true);
        prevAtk = weapon.atk1;
    }

    public void handleAttackSequence(Weapons weapon)
    {
        if (playerInputHandle.flagCombo)
        {
            animatorManager.animator.SetBool("canCombo", false);
            if (prevAtk.Equals(weapon.atk1))
            {
                animatorManager.PlayTargetAnimation(weapon.atk2, true);
            }
        }
    }
}
