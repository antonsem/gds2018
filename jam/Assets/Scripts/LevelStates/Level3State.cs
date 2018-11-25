using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Level3State : LevelStateController
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
            Events.Instance.playerDied.Invoke(DeathType.Spider);
            deadFrame = Time.frameCount;
            playerDead = true;
        }

        if(playerDead)
        { 
            if (Math.Abs(Time.frameCount - deadFrame) > 250)
            {
                Events.Instance.levelCompleted.Invoke();
            }
            
            if(virtualCamera.m_Lens.FieldOfView > 30)
            {
                virtualCamera.m_Lens.FieldOfView -= 0.1f;
            }
        }
    }
}
