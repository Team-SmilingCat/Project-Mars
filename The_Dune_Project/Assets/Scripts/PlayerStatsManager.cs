using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : EntityStatsManager
{
    [Header("player stats info")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerUIManager playerUIManager;
    [SerializeField] private int healthScale;

    private int healthLevel;
    private int maxHealth;
    [SerializeField] private int currHealth;

    [Header("Animator")] [SerializeField] private AnimatorManager animatorManager;

    private void Start()
    {
        InitStats();
    }

    public override void InitStats()
    {
        healthLevel = playerStats.maxHealth;
        maxHealth = healthLevel * healthScale;
        currHealth = maxHealth;
        playerUIManager.setMaxHealth(maxHealth);
    }

    //public methods
    public override void TakeDamage(int d)
    {
        currHealth -= damage;
        playerUIManager.SetHealth(currHealth);

        if (currHealth <= 0)
        {
            currHealth = 0;
            animatorManager.PlayTargetAnimation("death", true);
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    public override void gainHealth(int healthBonus)
    {
        return;
    }
}