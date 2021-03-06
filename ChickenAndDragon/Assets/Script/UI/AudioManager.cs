using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour {

    public AudioClip[] playlist;
    public AudioClip hurtSound;
    public AudioClip overSound;
    public AudioClip clickSound;
    public AudioSource audioSource;
    private int musicIndex = 0;

    public AudioMixerGroup soundEffectMixer;

    private static AudioManager instance;
    private AudioManager() { } //block the use of new()

    // Access point
    public static AudioManager Instance { get => instance; }

    void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        } else {
            instance = this;
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    private void Update() {
        if (!audioSource.isPlaying) {
            PlayNextSong();
        }
    }

    private void PlayNextSong() {
        musicIndex = (musicIndex +1) % playlist.Length;
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }

    public AudioSource PlayClip(AudioClip clip) {
        AudioSource tempAudioSource = gameObject.AddComponent<AudioSource>();
        tempAudioSource.clip = clip;
        tempAudioSource.outputAudioMixerGroup = soundEffectMixer;
        tempAudioSource.Play();
        Destroy(tempAudioSource, clip.length);
        return audioSource;
    }

    public void PlayOverSound() {
        AudioManager.Instance.PlayClip(overSound);
    }
    public void PlayClickSound() {
        AudioManager.Instance.PlayClip(clickSound);
    }
    public void PlayHurtSound() {
        AudioManager.Instance.PlayClip(hurtSound);
    }
}
