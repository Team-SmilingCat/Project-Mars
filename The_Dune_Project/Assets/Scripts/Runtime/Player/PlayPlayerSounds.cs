using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayPlayerSounds : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;

    [Header("sounds of footsteps")] [SerializeField]
    private FootStepsGroup[] footstepSounds;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

[SerializeField]
public struct FootStepsGroup
{
    public string type;
    public AudioClip[] stepSounds;
}
