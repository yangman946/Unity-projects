using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class settings : MonoBehaviour
{
    public AudioMixer audiomixer;
    public Dropdown dropdown;

    public void Start()
    {
        dropdown.value = QualitySettings.GetQualityLevel();
    }

    public void setvolume(float volume)
    {
        audiomixer.SetFloat("volume", volume);
    }

    public void setquality(int qualityindex)
    {
        QualitySettings.SetQualityLevel(qualityindex);
    }

}
