using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedMobEntity : EntityStatsManager
{
    [SerializeField] private EntityStats imperium;
    [SerializeField] private Animator animator;
    [SerializeField] private bool isHittable;
    public int currentHealth { get; private set; }
    public int damage { get; private set; }

    public bool isBeingHit;

    private void Awake()
    {
        InitStats();
    }

    public override void InitStats()
    {
        isHittable = true;
        isBeingHit = false;
        currentHealth = imperium.maxHealth;
        damage = imperium.damage;
    }

    public override void TakeDamage(int damage)
    {
        if (isHittable)
        {
            isBeingHit = true;
            animator.Play("hit");
            currentHealth -= damage;
            Debug.Log("Imperium health: " + currentHealth);
            if (currentHealth <= 0)
            {
                isBeingHit = true;
                animator.Play("death");
                isHittable = false;
                gameObject.GetComponent<CharacterController>().enabled = false;
            }

                StartCoroutine(Timer(5f));
        }
    }

    public override void gainHealth(int healthBonus)
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator Timer(float time)
    {
        yield return new WaitForSeconds(time);
        isBeingHit = false;
    }
}
