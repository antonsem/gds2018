using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStateController : MonoBehaviour
{
    public float musicVolPercentage = 0.6f;
    public float sfxVolPercentage = 1f;
    public string levelName;
    public string nextLevelName;
    [TextArea(10, 100)]
    public string tip;

    private bool quitting = false;

    protected virtual void OnEnable()
    {
        UIManager.Instance.SetPanel(Panel.None);
        Events.Instance.levelLoaded.Invoke(this);
        AudioManager.Instance.SetNormalizedMusicVolume(Settings.musicVolPercentage * musicVolPercentage);
        AudioManager.Instance.SetNormalizedSFXVolume(Settings.sfxVolPercentage * sfxVolPercentage);
    }

    protected virtual void OnDisable()
    {
        if (!quitting)
        {
            Events.Instance.levelLoaded.Invoke(null);
            AudioManager.Instance.SetNormalizedMusicVolume(Settings.musicVolPercentage);
            AudioManager.Instance.SetNormalizedSFXVolume(Settings.sfxVolPercentage);
        }
    }

    private void OnApplicationQuit()
    {
        quitting = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            Events.Instance.levelCompleted.Invoke();
        if (Input.GetKeyDown(KeyCode.F))
            Events.Instance.levelFailed.Invoke();
    }
}
