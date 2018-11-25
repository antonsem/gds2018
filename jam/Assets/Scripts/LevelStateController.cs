using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStateController : MonoBehaviour
{
    public float musicVolPercentage = 0.6f;
    public string levelName;
    public string nextLevelName;
    [TextArea(10, 100)]
    public string tip;

    private bool quitting = false;

    protected virtual void OnEnable()
    {
        Events.Instance.levelLoaded.Invoke(this);
        AudioManager.Instance.SetNormalizedMusicVolume(Settings.musicVolPercentage * musicVolPercentage);
    }

    protected virtual void OnDisable()
    {
        if (!quitting)
            AudioManager.Instance.SetNormalizedMusicVolume(Settings.musicVolPercentage);
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
