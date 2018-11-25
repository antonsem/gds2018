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

    public void SetNormalizedMasterVolume(float vol, bool setImmidiate = false)
    {
        vol = Mathf.Clamp01(vol);
        SetMasterVolume((vol - 1) * 80, setImmidiate);
    }

    public void SetNormalizedMusicVolume(float vol, bool setImmidiate = false)
    {
        vol = Mathf.Clamp01(vol);
        SetMusicVolume((vol - 1) * 80, setImmidiate);
    }

    public void SetNormalizedSFXVolume(float vol, bool setImmidiate = false)
    {
        vol = Mathf.Clamp01(vol);
        SetSFXVolume((vol - 1) * 80, setImmidiate);
    }

    public void SetMasterVolume(float vol, bool setImmidiate = false)
    {
        if (volCoroutine != null)
            StopCoroutine(volCoroutine);

        volCoroutine = SetVolumeCoroutine("MasterVol", vol, setImmidiate);

        StartCoroutine(volCoroutine);
    }

    public void SetMusicVolume(float vol, bool setImmidiate = false)
    {
        if (volCoroutine != null)
            StopCoroutine(volCoroutine);

        volCoroutine = SetVolumeCoroutine("MusicVol", vol, setImmidiate);

        StartCoroutine(volCoroutine);
    }

    public void SetSFXVolume(float vol, bool setImmidiate = false)
    {
        if (volCoroutine != null)
            StopCoroutine(volCoroutine);

        volCoroutine = SetVolumeCoroutine("SFXVol", vol, setImmidiate);

        StartCoroutine(volCoroutine);
    }

    public IEnumerator SetVolumeCoroutine(string volName, float vol, bool setImmidiate = false)
    {
        float currentVol = 0;
        mixer.GetFloat(volName, out currentVol);
        while (Mathf.Abs(currentVol - vol) > 0.01f && !setImmidiate)
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
