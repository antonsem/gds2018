using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Level2State : LevelStateController
{
    public GameObject Spider;

    private int deadFrame;

    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    private bool playerDead = false;

    private void Start()
    {
        if (Spider == null)
        {
            Spider = GameObject.Find("Spider");
        }
    }

    private void Update()
    {
        if (Spider.GetComponent<SpiderScript>().Bitted && !playerDead)
        {
            Events.Instance.playerDied.Invoke(DeathType.Explode);
            deadFrame = Time.frameCount;
            playerDead = true;
        }

        if(playerDead)
        { 
            if (Math.Abs(Time.frameCount - deadFrame) > 250)
            {
                Events.Instance.levelCompleted.Invoke();
            }
            
            if(virtualCamera.m_Lens.FieldOfView > 40)
            {
                virtualCamera.m_Lens.FieldOfView -= 0.1f;
            }
        }
    }
}
