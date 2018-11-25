using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum Panel
{
    None,
    Pause,
    GameOver,
    LevelCompleted,
    Loading,
    MainMenu,
    Fade,
    Options,
    Credits
}

public class UIManager : Singleton<UIManager>
{
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
    [SerializeField]
    private OptionsPanel optionsPanel;
    [SerializeField]
    private CreditsPanel creditsPanel;

    public Selectable currentButton;
    private Panel currentPanel = Panel.None;

    private EventSystem _sys;
    public EventSystem sys
    {
        get
        {
            if (!_sys)
                _sys = EventSystem.current;
            return _sys;
        }
    }

    private IEnumerator Enable()
    {
        yield return new WaitForEndOfFrame();
        currentButton = FindObjectOfType<Button>();
        if (currentButton)
            currentButton.Select();
        else
            Debug.LogError("Couldn't find a button, do you need it?");
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
        optionsPanel.gameObject.SetActive(type == Panel.Options);
        creditsPanel.gameObject.SetActive(type == Panel.Credits);

        currentPanel = type;
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
        Events.Instance.levelCompleted.AddListener(LevelCompleted);
        Events.Instance.levelFailed.AddListener(LevelFailed);
    }

    private void OnDisable()
    {
        if (!quitting)
        {
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
        optionsPanel = FindObjectOfType<OptionsPanel>();
        creditsPanel = FindObjectOfType<CreditsPanel>();
    }

    private void OnApplicationQuit()
    {
        quitting = true;
    }

    private void FixedUpdate()
    {
        if (!sys.currentSelectedGameObject || !sys.currentSelectedGameObject.activeInHierarchy)
        {
            if (currentPanel == Panel.MainMenu || currentPanel == Panel.Credits
                || currentPanel == Panel.GameOver || currentPanel == Panel.LevelCompleted
                || currentPanel == Panel.Options)
            {
                StartCoroutine(Enable());
            }
        }
    }
}
