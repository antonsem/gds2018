using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStateController : MonoBehaviour
{
    public string levelName;
    public string tip;
    public string nextLevelName;

    protected virtual void OnEnable()
    {
        Events.Instance.levelLoaded.Invoke(this);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            Events.Instance.levelCompleted.Invoke();
        if (Input.GetKeyDown(KeyCode.F))
            Events.Instance.levelFailed.Invoke();
    }
}
