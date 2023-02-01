using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class SettingsMenu : MonoBehaviour 
{
    [SerializeField] private AudioMixer audioMixer;
    public static float mouseSensitivity;




    public void SetVolume(float _volume)
    {
        audioMixer.SetFloat("volume", Mathf.Log10(_volume)*20);
    }

    public void SetSensitivity(float _sensitivity)
    {
        StaticVariables.mouseSensitivity = _sensitivity;
    }
}
