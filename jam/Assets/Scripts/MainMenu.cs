using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public int levelNo = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LevelManager.Instance.LoadLevel(levelNo, null, null);
            levelNo++;
        }
        if(Input.GetKeyDown(KeyCode.Backspace))
        {
            levelNo = 0;
            LevelManager.Instance.LoadMainScene(() => UIManager.Instance.SetPanel(Panel.MainMenu));
        }
    }
}
