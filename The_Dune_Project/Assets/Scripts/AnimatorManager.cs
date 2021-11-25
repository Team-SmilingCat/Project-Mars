using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorManager : MonoBehaviour
{
    public Animator animator;
    private int horizontal;
    private int vertical;
    

    private void Awake()
    {
        //animator = gameObject.GetComponent<Animator>();
        horizontal = Animator.StringToHash("Horizontal"); 
        vertical = Animator.StringToHash("Vertical");
    }
    
    public void PlayTargetAnimation(String targetAnimation, bool isInteracting)
    {
        animator.SetBool("isInteracting", isInteracting);
        animator.CrossFade(targetAnimation, 0.2f);
    }

    public void UpdateAnimatorValues(float horizontal, float vertical, bool isSprinting, bool isWalking)
    {
        float snapHorizontal;
        float snapVertical;

        #region snapHorizontal
        if (horizontal > 0 && horizontal < 0.55f)
        {
            snapHorizontal = 0.5f;
        } else if (horizontal > 0.55f)
        {
            snapHorizontal = 1f;
        } else if (horizontal < 0 && horizontal > -0.55f)
        {
            snapHorizontal = -0.5f;
        } else if (horizontal < -0.55f)
        {
            snapHorizontal = -1f;
        }
        else
        {
            snapHorizontal = 0f;
        }
        #endregion
        #region snapVertical
        if (vertical > 0 && vertical < 0.55f)
        {
            snapVertical = 0.5f;
        } else if (vertical > 0.55f)
        {
            snapVertical = 1f;
        } else if (vertical < 0 && vertical > -0.55f)
        {
            snapVertical = -0.5f;
        } else if (vertical < -0.55f)
        {
            snapVertical = -1f;
        }
        else
        {
            snapVertical = 0f;
        }
        #endregion
        
        if (isSprinting) 
        {
            snapHorizontal = horizontal;
            snapVertical = 2;
            
        } else if (isWalking)
        {
            snapHorizontal = horizontal;
            snapVertical = 0.5f;
        }
        animator.SetFloat(this.horizontal, snapHorizontal, 0.1f, Time.deltaTime);
        animator.SetFloat(this.vertical, snapVertical, 0.1f, Time.deltaTime);
    }

    public void EnableCombo()
    {
        animator.SetBool("canCombo", true);
    }

    public void DisableCombo()
    {
        animator.SetBool("canCombo", false);
    }

}
