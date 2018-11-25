using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip[] footSteps;
    [SerializeField]
    private AudioClip[] jumps;
    [SerializeField]
    private AudioClip[] lands;
    [SerializeField]
    private AudioClip[] explosions;
    [SerializeField]
    private AudioClip[] electricutions;
    [SerializeField]
    private AudioClip[] screams;
    [SerializeField]
    private AudioClip[] hang;

    public void PlayFootStep()
    {
        PlayFromList(footSteps);
    }

    public void PlayExplosion()
    {
        PlayFromList(explosions);
    }

    public void PlayElectricutions()
    {
        PlayFromList(electricutions);
    }

    public void PlayJump()
    {
        PlayFromList(jumps);
    }

    public void PlayLand()
    {
        PlayFromList(lands);
    }

    public void PlayScream()
    {
        PlayFromList(screams);
    }

    public void PlayHang()
    {
        PlayFromList(hang);
    }

    public void PlayFromList(AudioClip[] list, float minPitch = 0.95f, float maxPitch = 1.05f)
    {
        if (list != null && list.Length > 0)
        {
            source.pitch = Random.Range(minPitch, maxPitch);
            source.PlayOneShot(list[Random.Range(0, list.Length - 1)]);
        }
    }
}
