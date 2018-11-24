using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour
{
    [SerializeField]
    private Button newGame;

    private void Awake()
    {
        newGame.onClick.AddListener(OnNewGame);
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
