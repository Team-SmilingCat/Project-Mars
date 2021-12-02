using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public abstract class EntityStatsManager : MonoBehaviour
{

    public int health;
    public int damage;

    public abstract void InitStats();

    public abstract void TakeDamage(int damage);

    public abstract void gainHealth(int healthBonus);

    public virtual void KillEntity()
    {
        Destroy(this);
    }
    
}


