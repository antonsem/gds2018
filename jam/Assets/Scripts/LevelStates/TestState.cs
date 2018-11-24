using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestState : LevelStateController
{
    private float dieTime = 10;

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
