using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    private const string MASTER_VOLUME_PARAMETER = "MasterVolume";
    private const string SFX_VOLUME_PARAMETER = "SFXVolume";

    [SerializeField] private AudioMixer mixer;

    private float lastMasterValue;
    private float lastSFXValue;

    public void MasterVolumeSliderChanged(float newValue)
    {
        if (Mathf.Approximately(lastMasterValue, newValue))
            return;

        lastMasterValue = newValue;

        float actualVolumeValue = (1 - newValue) * -40f;
        mixer.SetFloat(MASTER_VOLUME_PARAMETER, actualVolumeValue);
    }
    
    public void SFXVolumeSliderChanged(float newValue)
    {
        if (Mathf.Approximately(lastSFXValue, newValue))
            return;

        lastSFXValue = newValue;

        float actualVolumeValue = (1 - newValue) * -40f;
        mixer.SetFloat (SFX_VOLUME_PARAMETER,actualVolumeValue);
    }
}
