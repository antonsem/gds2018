using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField]
    private Button newGame;
    [SerializeField]
    private Button options;
    [SerializeField]
    private Button credits;
    [SerializeField]
    private Button quit;

    private void Awake()
    {
        newGame.onClick.AddListener(OnNewGame);
        options.onClick.AddListener(OnOptions);
        credits.onClick.AddListener(OnCredits);
        quit.onClick.AddListener(OnQuit);
    }

    private void OnQuit()
    {
        Application.Quit();
    }

    private void OnCredits()
    {
        UIManager.Instance.SetPanel(Panel.Credits);
    }

    private void OnOptions()
    {
        UIManager.Instance.SetPanel(Panel.Options);
    }

    private void OnNewGame()
    {
        UIManager.Instance.FadeOut(LevelLoad);
    }

    private void LevelLoad()
    {
        LevelManager.Instance.LoadLevel(0, () => UIManager.Instance.FadeIn(null), null);
    }
}
