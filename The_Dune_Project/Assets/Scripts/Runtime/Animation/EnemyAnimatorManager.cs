using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimatorManager : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    public void PlayTargetAnimation(String targetAnimation, bool isInteracting)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnimation, 0.2f);
    }

    public void SetAnimBool(String anim, bool b)
    {
        animator.SetBool(anim, b);
    }

    public bool GetBoolData(string anim)
    {
        return animator.GetBool(anim);
    }
    
}
