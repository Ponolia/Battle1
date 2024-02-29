using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Option : MonoBehaviour
{
    public Slider myBackGroundSoundBar;
    public Slider myEffectSoundBar;
    public AudioSource myBackGroundSource;
    public AudioSource myEffectSource;
    // Start is called before the first frame update
    void Start()
    {
        myBackGroundSource = GameManager.Inst.SoundManager.myBackGroundSource;
        myEffectSource = GameManager.Inst.SoundManager.myEffectSource;
        myBackGroundSoundBar.value = myBackGroundSource.volume;
        myEffectSoundBar.value = myEffectSource.volume;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnBackGroundAudioVolume(float SliderValue)
    {
        myBackGroundSource.volume = Mathf.Clamp(SliderValue, 0.0f, 1.0f);
    }

    public void OnEffectAudioVolume(float SliderValue)
    {
        myEffectSource.volume = Mathf.Clamp(SliderValue, 0.0f, 1.0f);
    }
}
