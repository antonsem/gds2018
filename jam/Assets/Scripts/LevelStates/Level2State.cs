using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2State : LevelStateController
{
    private GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        // After tuch spider you die
    }
}
