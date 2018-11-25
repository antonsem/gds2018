using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditsPanel : MonoBehaviour
{
    [SerializeField]
    private Button mainMenu;

    private void Start()
    {
        mainMenu.onClick.AddListener(OnMainMenu);
    }

    private void OnMainMenu()
    {
        UIManager.Instance.SetPanel(Panel.MainMenu);
    }
}
