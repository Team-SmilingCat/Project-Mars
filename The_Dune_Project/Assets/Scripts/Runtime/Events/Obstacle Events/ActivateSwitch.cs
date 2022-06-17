using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSwitch : MonoBehaviour
{
    [SerializeField] private ActivateCutscene activateCutscene;

    void Start()
    {
        
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
        if(id == gameObject.GetInstanceID()) DetectHit();
    }

    private void DetectHit()
    {
        activateCutscene.ExecuteEvent();
        OnDisable();
    }
    
    
    
}
