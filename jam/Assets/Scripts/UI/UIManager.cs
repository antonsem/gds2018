using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Panel
{
    None,
    Pause,
    GameOver,
    LevelCompleted,
    Loading,
    MainMenu,
    Fade
}

public class UIManager : Singleton<UIManager>
{
    public LevelStateController currentLevel;
    private bool quitting = false;
    [SerializeField]
    private Panel defaultPanel = Panel.MainMenu;
    [SerializeField]
    private MainMenuPanel mainMenuPanel;
    [SerializeField]
    private GameOverPanel gameOverPanel;
    [SerializeField]
    private LevelCompletePanel levelCompletePanel;
    [SerializeField]
    private FadePanel fadePanel;

    private void SetCurrentLevel(LevelStateController level)
    {
        currentLevel = level;
    }

    public void FadeOut(Action callback)
    {
        fadePanel.FadeOut(() =>
        {
            if (callback != null)
                callback.Invoke();
            SetPanel(Panel.None);
        });
    }

    public void FadeIn(Action callback)
    {
        fadePanel.FadeIn(callback);
    }

    private IEnumerator FadeInCoroutine(Action callback)
    {
        yield return null;
        if (callback != null)
            callback.Invoke();
    }

    public void SetPanel(Panel type)
    {
        mainMenuPanel.gameObject.SetActive(type == Panel.MainMenu);
        gameOverPanel.gameObject.SetActive(type == Panel.GameOver);
        levelCompletePanel.gameObject.SetActive(type == Panel.LevelCompleted);
    }

    private void LevelFailed()
    {
        SetPanel(Panel.GameOver);
    }

    private void LevelCompleted()
    {
        SetPanel(Panel.LevelCompleted);
    }

    private void Start()
    {
        SetPanel(defaultPanel);
    }

    private void OnEnable()
    {
        Events.Instance.levelLoaded.AddListener(SetCurrentLevel);
        Events.Instance.levelCompleted.AddListener(LevelCompleted);
        Events.Instance.levelFailed.AddListener(LevelFailed);
    }

    private void OnDisable()
    {
        if (!quitting)
        {
            Events.Instance.levelLoaded.RemoveListener(SetCurrentLevel);
            Events.Instance.levelCompleted.AddListener(LevelCompleted);
            Events.Instance.levelFailed.AddListener(LevelFailed);
        }
    }

    private void Reset()
    {
        mainMenuPanel = FindObjectOfType<MainMenuPanel>();
        gameOverPanel = FindObjectOfType<GameOverPanel>();
        levelCompletePanel = FindObjectOfType<LevelCompletePanel>();
        fadePanel = FindObjectOfType<FadePanel>();
    }

    private void OnApplicationQuit()
    {
        quitting = true;
    }
}
