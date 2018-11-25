using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField]
    private AudioSource musicSource;
    [SerializeField]
    private AudioSource sfxSource;
    [SerializeField]
    private AudioMixer mixer;
    private IEnumerator volCoroutine = null;

    public void SetNormalizedMasterVolume(float vol)
    {
        vol = Mathf.Clamp01(vol);
        SetMasterVolume((vol - 1) * 80);
    }

    public void SetNormalizedMusicVolume(float vol)
    {
        vol = Mathf.Clamp01(vol);
        SetMusicVolume((vol - 1) * 80);
    }

    public void SetNormalizedSFXVolume(float vol)
    {
        vol = Mathf.Clamp01(vol);
        SetSFXVolume((vol - 1) * 80);
    }

    public void SetMasterVolume(float vol)
    {
        if (volCoroutine != null)
            StopCoroutine(volCoroutine);

        volCoroutine = SetVolumeCoroutine("MasterVol", vol);

        StartCoroutine(volCoroutine);
    }

    public void SetMusicVolume(float vol)
    {
        if (volCoroutine != null)
            StopCoroutine(volCoroutine);

        volCoroutine = SetVolumeCoroutine("MusicVol", vol);

        StartCoroutine(SetVolumeCoroutine("MusicVol", vol));
    }

    public void SetSFXVolume(float vol)
    {
        if (volCoroutine != null)
            StopCoroutine(volCoroutine);

        volCoroutine = SetVolumeCoroutine("SFXVol", vol);

        StartCoroutine(SetVolumeCoroutine("SFXVol", vol));
    }

    public IEnumerator SetVolumeCoroutine(string volName, float vol)
    {
        float currentVol = 0;
        mixer.GetFloat(volName, out currentVol);
        while (Mathf.Abs(currentVol - vol) > 0.01f)
        {
            mixer.SetFloat(volName, Mathf.Lerp(currentVol, vol, Time.deltaTime * 5));
            mixer.GetFloat(volName, out currentVol);
            yield return null;
        }
        mixer.SetFloat(volName, vol);
    }

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
