using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : MonoBehaviour
{
    public Cinemachine.CinemachineVirtualCamera virtualCamera;

    public float ZoomTo = 45f;

    public bool zoomingIn;

    void Start()
    {
        
    }
    
    void Update()
    {
        if (zoomingIn)
        {
            if (virtualCamera.m_Lens.FieldOfView > ZoomTo)
            {
                virtualCamera.m_Lens.FieldOfView -= 0.4f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        zoomingIn = true;
    }
}
