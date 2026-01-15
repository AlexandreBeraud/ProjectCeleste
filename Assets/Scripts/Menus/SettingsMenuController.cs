using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenuController : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }
}
