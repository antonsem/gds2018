using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1State : LevelStateController
{
    public GameObject SpikesArea;

    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    private bool playerDead = false;
    private GameObject player;
    private int deadFrame;

    private void Start()
    {
        player = GameObject.Find("Player");

        if (SpikesArea == null)
        {
            SpikesArea = GameObject.Find("SpikesArea");
        }
    }

    private void Update()
    {
        if(SpikesArea.GetComponent<Spikes>().Spiked && !playerDead)
        {
            player.GetComponent<PlayerInput>().enabled = false;
            Events.Instance.playerDied.Invoke(DeathType.Explode);
            deadFrame = Time.frameCount;
            playerDead = true;
        }

        if (playerDead)
        {
            if (Math.Abs(Time.frameCount - deadFrame) > 180)
            {
                Events.Instance.levelCompleted.Invoke();
            }

            if (virtualCamera.m_Lens.FieldOfView > 30)
            {
                virtualCamera.m_Lens.FieldOfView -= 0.1f;
            }
        }
    }
}
