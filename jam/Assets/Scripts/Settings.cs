using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : Singleton<Settings>
{
    private static readonly string masterVol = "MasterVol";
    private static readonly string musicVol = "MusicVol";
    private static readonly string sfxVol = "SFXVol";

    public static float masterVolPercentage = 1;
    public static float musicVolPercentage = 1;
    public static float sfxVolPercentage = 1;

    public override void Init()
    {
        if (PlayerPrefs.HasKey(masterVol))
            masterVolPercentage = PlayerPrefs.GetFloat(masterVol);
        if (PlayerPrefs.HasKey(musicVol))
            musicVolPercentage = PlayerPrefs.GetFloat(musicVol);
        if (PlayerPrefs.HasKey(sfxVol))
            sfxVolPercentage = PlayerPrefs.GetFloat(sfxVol);
    }

    public static void SaveSettings()
    {
        PlayerPrefs.SetFloat(masterVol, masterVolPercentage);
        PlayerPrefs.SetFloat(musicVol, musicVolPercentage);
        PlayerPrefs.SetFloat(sfxVol, sfxVolPercentage);
    }
}
