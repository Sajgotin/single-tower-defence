using System.Collections;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class AudioSettings : MonoBehaviour
{
    [SerializeField] AudioMixer _mainMixer;
    [SerializeField] Slider _masterSlider;
    [SerializeField] Slider _musicSlider;
    [SerializeField] Slider _effectsSlider;
    private string MASTER_VOLUME = "MasterVolume";
    private string MUSIC_VOLUME = "MusicVolume";
    private string EFFECTS_VOLUME = "EffectsVolume";

    private void Awake()
    {
        Load();
        transform.parent.gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(_masterSlider.gameObject);
    }

    public void SetMasterVolume()
    {
        _mainMixer.SetFloat(MASTER_VOLUME, Mathf.Log(_masterSlider.value) * 20f);
        SaveSystem.SaveData(MASTER_VOLUME, _masterSlider.value);
    }

    public void SetMusicVolume()
    {
        _mainMixer.SetFloat(MUSIC_VOLUME, Mathf.Log(_musicSlider.value) * 20f);
        SaveSystem.SaveData(MUSIC_VOLUME, _musicSlider.value);
    }

    public void SetEffectsVolume()
    {
        _mainMixer.SetFloat(EFFECTS_VOLUME, Mathf.Log(_effectsSlider.value) * 20f);
        SaveSystem.SaveData(EFFECTS_VOLUME, _effectsSlider.value);
    }

    void Load()
    {
        _masterSlider.value = SaveSystem.LoadData(MASTER_VOLUME, _masterSlider.value);
        _musicSlider.value = SaveSystem.LoadData(MUSIC_VOLUME, _musicSlider.value);
        _effectsSlider.value = SaveSystem.LoadData(EFFECTS_VOLUME, _effectsSlider.value);
    }
}
