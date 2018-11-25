using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level3State : LevelStateController
{
    private GameObject player;
    
    private float dieTime = 10;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        dieTime -= Time.deltaTime;
        if (dieTime < 0)
        {
            Events.Instance.playerDied.Invoke(DeathType.Explode);
            Events.Instance.levelCompleted.Invoke();
        }
    }
}
