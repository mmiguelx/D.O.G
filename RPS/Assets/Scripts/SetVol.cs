using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SetVol : MonoBehaviour
{
    public AudioMixer mixer;
    static float value;
    static bool firstRun = true;

    void Start()
    {
        var slider = GetComponent<Slider>();
        if (value != 1 && firstRun == false)
        {
            slider.value = value;
        }
        else if (firstRun == true)
        {
            slider.value = 1;
            firstRun = false;
        }
    }

    void Update()
    {
        var slider = GetComponent<Slider>();
        if (slider.value != 1)
        {
            value = slider.value;
        }
    }

    public void SetLevel (float sliderValue)
    {
        mixer.SetFloat ("MusicVol", Mathf.Log10(sliderValue) * 20);
    }
}
