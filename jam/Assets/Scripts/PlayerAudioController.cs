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
    private AudioClip[] explosions;

    public void PlayFootStep()
    {
        if (footSteps != null && footSteps.Length > 0)
        {
            source.pitch = Random.Range(0.95f, 1.05f);
            source.PlayOneShot(footSteps[Random.Range(0, footSteps.Length - 1)]);
        }
    }

    public void PlayExplosion()
    {
        if (explosions != null && explosions.Length > 0)
        {
            source.pitch = Random.Range(0.95f, 1.05f);
            source.PlayOneShot(explosions[Random.Range(0, explosions.Length - 1)]);
        }
    }
}
