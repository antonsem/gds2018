using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Head : MonoBehaviour
{
    [SerializeField]
    private AudioSource source;
    [SerializeField]
    private AudioClip[] hit;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayFromList(hit);
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
