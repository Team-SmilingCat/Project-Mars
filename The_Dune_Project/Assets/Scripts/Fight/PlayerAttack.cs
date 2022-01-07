using System.Collections;
using System.Collections.Generic;
using Scriptable_Objects;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private AnimatorManager animatorManager;
    [SerializeField] private PlayerInputHandle playerInputHandle;
    
    [Header("attack settings")] private string prevAtk;
    
    public void handleMeleeAttack(MeleeWeapon weapon)
    {
        animatorManager.PlayTargetAnimation(weapon.atk1, true);
            prevAtk = weapon.atk1;
    }

    public void HandleHeavyMeleeAttack(MeleeWeapon weapon)
    {
        animatorManager.PlayTargetAnimation(weapon.atk4, true);
        prevAtk = weapon.atk4;
    }

    public void handleMeleeAttackSequence(MeleeWeapon weapon)
    {
        if (playerInputHandle.flagCombo)
        {
            animatorManager.animator.SetBool("canCombo", false);
            if (prevAtk.Equals(weapon.atk1))
            {
                animatorManager.PlayTargetAnimation(weapon.atk2, true);
                prevAtk = weapon.atk2;
            } 
            else if (prevAtk.Equals(weapon.atk2))
            {
                animatorManager.PlayTargetAnimation(weapon.atk3, true);
            }
        }
    }
}
