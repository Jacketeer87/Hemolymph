using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.Rendering;

public class OptionsMenu : MonoBehaviour
{
    [Header ("Audio")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider masterVolSlider;
    [SerializeField] Slider musicVolSlider;
    [SerializeField] Slider SFXVolSlider;

    [SerializeField] Canvas canvas;

    [Header ("Resolution")]
    [SerializeField] TMP_Dropdown resDropdown;
    Resolution[] resolutions;
    [SerializeField] Toggle fullscreenToggle;
    // Start is called before the first frame update
    void Start()
    {
        masterVolSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        musicVolSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        SFXVolSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        GetResOptions();
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

    void GetResOptions(){
        resDropdown.ClearOptions();
        resolutions = Screen.resolutions;
        for(int i = 0; i<resolutions.Length; i++){
            TMP_Dropdown.OptionData newOption;
            newOption = new TMP_Dropdown.OptionData(resolutions[i].width.ToString() + "x" + resolutions[i].height.ToString());
            resDropdown.options.Add(newOption);
        }
    }

    public void ChooseRes()
    {
        Screen.SetResolution(resolutions[resDropdown.value].width, resolutions[resDropdown.value].height, fullscreenToggle.isOn);
    }

    public void OpenOptions(){
        canvas.enabled = true;
    }

    public void CloseOptions(){
        canvas.enabled = false;
    }
}
