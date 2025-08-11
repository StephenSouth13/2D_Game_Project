using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettingGame : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private AudioSource musicSource;

    private void Start()
    {
        // Lấy giá trị âm lượng đã lưu
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("MusicVolume");
            musicSlider.value = savedVolume;
            SetMusicVolume(savedVolume);
        }
        else
        {
            musicSlider.value = 0.75f;
            SetMusicVolume(0.75f);
        }

        // Bắt đầu phát nhạc
        if (musicSource != null && !musicSource.isPlaying)
            musicSource.Play();
    }

    public void OnMusicSliderChanged()
    {
        SetMusicVolume(musicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }

    private void SetMusicVolume(float volume)
    {
        // Chuyển từ 0-1 sang dB (-80 đến 0)
        audioMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
    }
}
