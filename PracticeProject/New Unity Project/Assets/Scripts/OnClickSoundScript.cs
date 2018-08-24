using UnityEngine;
using UnityEngine.UI;

public class OnClickSoundScript : MonoBehaviour
{
    [Tooltip("The sound to play on click.")]
    [SerializeField]
    private AudioClip soundToPlay = null;

    private Button button;
    private AudioSource audioSource;

    private void Awake()
    {
        gameObject.AddComponent<AudioSource>();

        audioSource = GetComponent<AudioSource>();
        button = GetComponent<Button>();

        audioSource.clip = soundToPlay;
        audioSource.playOnAwake = false;
        button.onClick.AddListener(() => PlaySound());
    }

    public void PlaySound()
    {
        audioSource.PlayOneShot(soundToPlay);
    }
}
