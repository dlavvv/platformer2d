using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance {  get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;

    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        // singleton pattern - only one instance of SoundManager is allowed
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // if another instance exists, destory it
        else if (instance != null && instance != this)
            Destroy(gameObject);

        if (!PlayerPrefs.HasKey("soundVolume"))
            PlayerPrefs.SetInt("soundVolume", 100);
        if (!PlayerPrefs.HasKey("musicVolume"))
            PlayerPrefs.SetInt("musicVolume", 50);

        ChangeSoundVolume(0);
        ChangeMusicVolume(0);
    }

    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound);
    }

    public void ChangeSoundVolume(int _change)
    {
        int currVol = PlayerPrefs.GetInt("soundVolume", 100);
        currVol += _change;

        if(currVol > 100)
            currVol = 0;
        else if(currVol < 0)
            currVol = 100;

        float finalVol = currVol / 100f;
        soundSource.volume = finalVol;

        PlayerPrefs.SetInt("soundVolume", currVol);
    }

    public void ChangeMusicVolume(int _change)
    {
        int currVol = PlayerPrefs.GetInt("musicVolume", 100);
        currVol += _change;

        if (currVol > 100)
            currVol = 0;
        else if (currVol < 0)
            currVol = 100;

        float finalVol = currVol / 100f;
        musicSource.volume = finalVol;

        PlayerPrefs.SetInt("musicVolume", currVol);
    }
}
