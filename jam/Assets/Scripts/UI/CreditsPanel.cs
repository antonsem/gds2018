using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class CreditsPanel : MonoBehaviour
{
    [SerializeField]
    private Button mainMenu;
    [SerializeField]
    private VideoPlayer player;

    private void OnEnable()
    {
        player.Play();
    }

    private void OnDisable()
    {
        player.Pause();
    }

    private void Start()
    {
        mainMenu.onClick.AddListener(OnMainMenu);
    }

    private void OnMainMenu()
    {
        UIManager.Instance.SetPanel(Panel.MainMenu);
    }
}
