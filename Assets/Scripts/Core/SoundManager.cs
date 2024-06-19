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

        // keep this object even when we switch to a new scene
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // destroy duplicate gameobjects
        else if (instance != null && instance != this)
            Destroy(gameObject);

        ChangeSoundVolume(0);
        ChangeMusicVolume(0);
    }

    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound);
    }

    public void ChangeSoundVolume(float _change)
    {
        float baseVolume = 1;

        float currVol = PlayerPrefs.GetFloat("soundVolume", 1);
        currVol += _change;

        if(currVol > 1)
            currVol = 0;
        else if(currVol < 0)
            currVol = 1;

        float finalVol = currVol * baseVolume;
        soundSource.volume = finalVol;

        PlayerPrefs.SetFloat("soundVolume", currVol);
    }

    public void ChangeMusicVolume(float _change)
    {
        float baseVolume = 0.3f;

        float currVol = PlayerPrefs.GetFloat("musicVolume", 1);
        currVol += _change;

        if (currVol > 1)
            currVol = 0;
        else if (currVol < 0)
            currVol = 1;

        float finalVol = currVol * baseVolume;
        musicSource.volume = finalVol;

        PlayerPrefs.SetFloat("musicVolume", currVol);
    }
}
