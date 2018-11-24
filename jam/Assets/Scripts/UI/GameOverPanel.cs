using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanel : MonoBehaviour
{
    [SerializeField]
    private Button restart;
    [SerializeField]
    private Button mainMenu;

    private void Awake()
    {
        mainMenu.onClick.AddListener(OnMainMenu);
        restart.onClick.AddListener(OnRestart);
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
