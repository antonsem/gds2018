using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField]
    private Button restart;
    [SerializeField]
    private Button mainMenu;
    [SerializeField]
    private VideoPlayer video;
    [SerializeField]
    private TextMeshProUGUI levelName;
    [SerializeField]
    private TextMeshProUGUI tip;

    private void Awake()
    {
        mainMenu.onClick.AddListener(OnMainMenu);
        restart.onClick.AddListener(OnRestart);
    }

    private void OnEnable()
    {
        if (UIManager.Instance.currentLevel)
        {
            levelName.text = UIManager.Instance.currentLevel.levelName;
            tip.text = UIManager.Instance.currentLevel.tip;
        }
        video.Play();
        AudioManager.Instance.SetNormalizedMusicVolume(Settings.musicVolPercentage);
    }

    private void OnDisable()
    {
        video.Pause();
    }

    private void OnRestart()
    {
        UIManager.Instance.FadeOut(Reload);
    }

    private void Reload()
    {
        LevelManager.Instance.ReloadLevel(() => UIManager.Instance.FadeIn(null), null);
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
