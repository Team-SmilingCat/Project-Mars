using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleEvents : MonoBehaviour
{
    public static ObstacleEvents current;
    public event Action TriggerBridgeCutScene;
    private ActivateCutscene activateCutscene;
    private void Awake()
    {
        current = this;
    }
    
    
}
