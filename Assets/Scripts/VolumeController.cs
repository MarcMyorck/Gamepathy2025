using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeController : MonoBehaviour
{
    public AudioMixer mixer;        // assign in Inspector
    public Slider volumeSlider;     // assign in Inspector

    const string exposedParam = "MasterVolume"; // name you exposed in the mixer
    const string prefsKey = "MasterVolume";

    void Start()
    {
        // load saved linear value (0..1), default 1
        float saved = PlayerPrefs.GetFloat(prefsKey, 1f);
        volumeSlider.value = saved;
        volumeSlider.onValueChanged.AddListener(SetVolume);
        // apply at start
        SetVolume(saved);
    }

    public void SetVolume(float sliderValue)
    {
        // convert linear 0..1 to dB (-80 = silent, 0 = normal)
        // clamp so Mathf.Log10 won't go negative infinity
        float minDb = -80f;
        float dB;
        if (sliderValue <= 0.0001f) dB = minDb;
        else dB = 20f * Mathf.Log10(sliderValue);

        mixer.SetFloat(exposedParam, dB);
        PlayerPrefs.SetFloat(prefsKey, sliderValue);
    }
}
