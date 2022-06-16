using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Requirement for progress/level requirement")]
public class LevelRequirement : ScriptableObject
{
    [SerializeField] private List<Requirement> listOfRequirementsForlevel;
    [SerializeField] private bool clearFlag;
    public string sectionName;
    
    
    public List<Requirement> GetRequirements()
    {
        return this.listOfRequirementsForlevel;
    }

    public void SetClearFlag(bool b)
    {
        clearFlag = b;
    }
}

[Serializable]
public struct Requirement
{
    public String name;
    public bool flagStatus;
}

