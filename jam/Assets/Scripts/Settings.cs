using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : Singleton<Settings>
{
    public static readonly string masterVol = "MasterVol";
    public static readonly string musicVol = "MusicVol";

    public static float masterVolPercentage = 1;
    public static float musicVolPercentage = 1;

    public override void Init()
    {
        if (PlayerPrefs.HasKey(masterVol))
            masterVolPercentage = PlayerPrefs.GetFloat(masterVol);
        if (PlayerPrefs.HasKey(musicVol))
            musicVolPercentage = PlayerPrefs.GetFloat(musicVol);
    }

    public static void SaveSettings()
    {
        PlayerPrefs.SetFloat(masterVol, masterVolPercentage);
        PlayerPrefs.SetFloat(musicVol, musicVolPercentage);
    }
}
