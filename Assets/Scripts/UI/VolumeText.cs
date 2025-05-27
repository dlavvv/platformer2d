using UnityEngine;
using UnityEngine.UI;

public class VolumeText : MonoBehaviour
{
    [SerializeField] private string volumeName;
    [SerializeField] private string description; // sound: or music:
    private Text text;

    private void Awake()
    {
        text = GetComponent<Text>();
    }

    private void Update()
    {
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        int volumeValue = PlayerPrefs.GetInt(volumeName, 100);
        text.text = description + volumeValue.ToString();
    }
}
