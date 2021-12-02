using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMobCombat : MonoBehaviour
{
    [SerializeField] private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Transform target = GameObject.FindWithTag("Player").transform;

        if (transform.position.x - target.transform.position.x < 50 && !GetComponent<RangedMobEntity>().isBeingHit && 
            GetComponent<RangedMobEntity>().currentHealth > 0)
        {
            animator.Play("shoot");
        }
        
    }
}
