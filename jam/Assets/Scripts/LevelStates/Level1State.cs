using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1State : LevelStateController
{
    public GameObject SpikesArea;

    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    private GameObject player;

    public static bool zoomCamera;

    private void Start()
    {
        player = GameObject.Find("Player");

        if (SpikesArea == null)
        {
            SpikesArea = GameObject.Find("SpikesKillArea");
        }
    }

    private void Update()
    {
        if (zoomCamera)
        {
            if (virtualCamera.m_Lens.FieldOfView > 30)
            {
                virtualCamera.m_Lens.FieldOfView -= 0.1f;
            }
        }
    }
}
