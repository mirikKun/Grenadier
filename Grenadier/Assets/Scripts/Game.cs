using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private UIStateSwitch uiStateSwitch;
    [SerializeField] private GunShooter gunShooter;
    [SerializeField] private GrenadeMover grenadeMover;
    [SerializeField] private ProjectilePool projectilesPool;
    [SerializeField] private ExplosionsPool explosionsPool;
    private bool _gameRunning = false;

    private void Start()
    {
        uiStateSwitch.StartBehaviour();
        gunShooter.StartBehaviour();
    }

    private void OnEnable()
    {
        uiStateSwitch.OnPauseGame += PauseGame;
        uiStateSwitch.OnContinueGame += ContinueGame;
    }

    private void OnDisable()
    {
        uiStateSwitch.OnPauseGame -= PauseGame;
        uiStateSwitch.OnContinueGame -= ContinueGame;
    }

    private void ContinueGame()
    {
        _gameRunning = true;
    }

    private void PauseGame()
    {
        _gameRunning = false;
    }

    private void Update()
    {
        if (!_gameRunning)
        {
            return;
        }
        
        gunShooter.GameUpdate();
        grenadeMover.GameUpdate();

        UpdateProjectiles();
        UpdateExplosions();
    }

    private void UpdateExplosions()
    {
        for (var i = 0; i < explosionsPool.GetActiveElements.Count; i++)
        {
            var activeExplosions = explosionsPool.GetActiveElements[i];
            if (!activeExplosions.GameUpdate())
            {
                i--;
            }
        }
    }
    private void UpdateProjectiles()
    {
        for (var i = 0; i < projectilesPool.GetActiveElements.Count; i++)
        {
            var activeProjectiles = projectilesPool.GetActiveElements[i];
            if (!activeProjectiles.GameUpdate())
            {
                i--;
            }
        }
    }
}