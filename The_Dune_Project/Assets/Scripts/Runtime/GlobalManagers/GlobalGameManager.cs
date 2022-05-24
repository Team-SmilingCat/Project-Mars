using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameManager : MonoBehaviour
{
    [SerializeField] private PlayerManager playerManager;
    public enum GameStates
    {
        StartGame,
        InCutScene,
        InMenu,
        MainLoop
    };

    public GameStates currentState;
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = GameStates.MainLoop;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case GameStates.StartGame:
                break;
            case GameStates.InMenu:
                break;
            case GameStates.InCutScene:
                break;
            case GameStates.MainLoop:
                playerManager.PlayerLoop();
                break;
        }
    }

    private void LateUpdate()
    {
        switch (currentState)
        {
            case GameStates.StartGame:
                break;
            case GameStates.InMenu:
                break;
            case GameStates.InCutScene:
                break;
            case GameStates.MainLoop:
                playerManager.PlayerLateUpdate();
                break;
        }
    }
    
    
    
    /* public methods available */

    public void SwitchStates(GameStates state)
    {
        currentState = state;
    }
}
