using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class LevelCompletePanel : MonoBehaviour
{
    [SerializeField]
    private Button next;
    [SerializeField]
    private Button mainMenu;
    [SerializeField]
    private VideoPlayer video;

    private void OnEnable()
    {
        video.Play();
        AudioManager.Instance.SetNormalizedMusicVolume(Settings.musicVolPercentage);
        AudioManager.Instance.SetNormalizedSFXVolume(0);
    }

    private void OnDisable()
    {
        video.Pause();
    }

    private void Start()
    {
        mainMenu.onClick.AddListener(OnMainMenu);
        next.onClick.AddListener(OnNext);
    }

    private void OnNext()
    {
        UIManager.Instance.FadeOut(LoadNext);
    }

    private void LoadNext()
    {
        LevelManager.Instance.LoadLevel(UIManager.Instance.currentLevel.nextLevelName,
            () => UIManager.Instance.FadeIn(null), null);
    }

    private void OnMainMenu()
    {
        UIManager.Instance.FadeOut(MainMenuLoad);
    }

    private void MainMenuLoad()
    {
        //Callback-ception :( sorry about that...
        LevelManager.Instance.LoadMainScene(() =>
            UIManager.Instance.FadeIn(() => UIManager.Instance.SetPanel(Panel.MainMenu))
        );
    }
}
