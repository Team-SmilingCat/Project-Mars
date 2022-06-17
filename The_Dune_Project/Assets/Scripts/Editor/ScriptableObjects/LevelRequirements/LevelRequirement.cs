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

    public void SetFlagForRequirement(string requirementName, bool b)
    {
        foreach(Requirement r in listOfRequirementsForlevel)
        {
            if(r.name.Equals(requirementName))
            {
                SetFlag(r, b);
            }
            else
            {
                Debug.Log($"Requirement not found");
            }
        }
    }

    private void SetFlag(Requirement r, bool b)
    {
        r.flagStatus = b;
    }
}

[Serializable]
public struct Requirement
{
    public String name;
    public bool flagStatus;
}

