using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class UI_VolumeSlider : MonoBehaviour,ISaveManager
{
    public Slider slider => GetComponent<Slider>();
    public string parameter;

    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float multiplier = 20;

    public void SliderValue(float _value)
    {
        float clampedValue = Mathf.Clamp(_value, 0.0001f, 1f);
        float volumeInDB = Mathf.Log10(clampedValue) * multiplier;

        audioMixer.SetFloat(parameter, volumeInDB);
    }

    public void LoadData(GameData _data)
    {
        if (parameter == "SFX")
            slider.value = _data.SFX_Volume;
        if (parameter == "BGM")
            slider.value = _data.BGM_Volume;
    }

    public void SaveData(ref GameData _data)
    {
        if (parameter == "SFX")
            _data.SFX_Volume = slider.value;
        if (parameter == "BGM") 
            _data.BGM_Volume = slider.value;
    }
}
