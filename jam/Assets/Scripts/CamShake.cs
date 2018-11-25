using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CinemachineImpulseSource))]
public class CamShake : Singleton<CamShake>
{
    [SerializeField]
    private CinemachineImpulseSource source;

    public override void Init()
    {
        Events.Instance.shakeCamera.AddListener(source.GenerateImpulse);
    }
}
