using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionsMenu : MonoBehaviour
{
    [Header ("Audio")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterVolSlider;
    [SerializeField] Slider musicVolSlider;
    [SerializeField] Slider SFXVolSlider;
    [SerializeField] Canvas canvas;
    // Start is called before the first frame update
    void Start()
    {
        masterVolSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        musicVolSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SFXVolSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMasterVolume(){
        audioMixer.SetFloat("MasterVolume", ConvertToDec(masterVolSlider.value));
        PlayerPrefs.SetFloat("MasterVolume", masterVolSlider.value);
    }

    public void SetMusicVolume(){
        audioMixer.SetFloat("MusicVolume", ConvertToDec(musicVolSlider.value));
        PlayerPrefs.SetFloat("MusicVolume", musicVolSlider.value);
    }

    public void SetSFXVolume(){
        audioMixer.SetFloat("SFXVolume", ConvertToDec(SFXVolSlider.value));
        PlayerPrefs.SetFloat("SFXVolume", SFXVolSlider.value);
    }

    float ConvertToDec(float sliderValue){
        return Mathf.Log10(Mathf.Max(sliderValue, 0.0001f))*20;
    }

    public void OpenOptions(){
        canvas.enabled = true;
    }

    public void CloseOptions(){
        canvas.enabled = false;
    }
}
