﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2State : LevelStateController
{
    private GameObject player;

    private bool timeComes = false;
    private float dieTime = 10;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        // After tuch spider you die
        if (timeComes)
        {
            Events.Instance.playerDied.Invoke(DeathType.Explode);
            Events.Instance.levelCompleted.Invoke();
        }

        dieTime -= Time.deltaTime;
        if (dieTime < 0)
        {
            Events.Instance.playerDied.Invoke(DeathType.Explode);
            Events.Instance.levelCompleted.Invoke();
        }
    }
}
