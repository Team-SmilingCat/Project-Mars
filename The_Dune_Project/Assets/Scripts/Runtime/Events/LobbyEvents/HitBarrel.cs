using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBarrel : MonoBehaviour
{
    [SerializeField] TriggerEvent triggerEvent;
    [SerializeField] Rigidbody tombToDrop;

    public void FreeObject()
    {
        tombToDrop.useGravity = true;
        OnDisable();
     
    }


    private void OnEnable()
    {
        RangedShootingHandler.OnHitEvent += OnHit;

    }

    private void OnDisable()
    {
        RangedShootingHandler.OnHitEvent -= OnHit;
    }

    private void OnHit(RangedShootingHandler r, int id)
    {
        if (id == gameObject.GetInstanceID())
        {
            triggerEvent.OnNotify();
        }
    }



}
