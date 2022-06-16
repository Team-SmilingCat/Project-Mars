using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [Header("external")] 
    [SerializeField] private GlobalGameManager gameManager;
    
    
    [SerializeField] private List<LevelRequirement> requirements;

    [SerializeField] private List<LevelRequirement> levelData;

    // Start is called before the first frame update
    void Awake()
    {
        
    }

    //should only be called at the beginning of scene
    public void LoadLevelData()
    {
        foreach (LevelRequirement l in requirements)
        {
            levelData.Add(l);
        }
        
        Debug.Log($"Loaded Level Data");
    }
    

}
