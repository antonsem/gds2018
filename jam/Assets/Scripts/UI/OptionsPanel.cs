using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsPanel : MonoBehaviour
{
    [SerializeField]
    private Button mainMenu;
    [SerializeField]
    private Button save;
    [SerializeField]
    private Button cancel;
    [SerializeField]
    private Slider masterVolume;
    [SerializeField]
    private Slider musicVolume;
    [SerializeField]
    private Slider sfxVolume;

    private void Start()
    {
        OnCancel();
        masterVolume.onValueChanged.AddListener(OnMasterChanged);
        musicVolume.onValueChanged.AddListener(OnMusicChanged);
        sfxVolume.onValueChanged.AddListener(OnSFXChanged);

        save.onClick.AddListener(OnSave);
        cancel.onClick.AddListener(OnCancel);
        mainMenu.onClick.AddListener(OnMainMenu);
    }

    private void OnSave()
    {
        Settings.masterVolPercentage = masterVolume.value;
        Settings.musicVolPercentage = musicVolume.value;
        Settings.sfxVolPercentage = sfxVolume.value;
        Settings.SaveSettings();
    }

    private void OnCancel()
    {
        masterVolume.value = Settings.masterVolPercentage;
        musicVolume.value = Settings.musicVolPercentage;
        sfxVolume.value = Settings.sfxVolPercentage;
    }

    private void OnMainMenu()
    {
        UIManager.Instance.SetPanel(Panel.MainMenu);
    }

    private void OnMasterChanged(float val)
    {
        AudioManager.Instance.SetNormalizedMasterVolume(val, true);
    }

    private void OnMusicChanged(float val)
    {
        AudioManager.Instance.SetNormalizedMusicVolume(val, true);
    }

    private void OnSFXChanged(float val)
    {
        AudioManager.Instance.SetNormalizedSFXVolume(val, true);
    }

}
