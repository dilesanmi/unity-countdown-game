using UnityEngine;

namespace SoundControl
{
    public class MusicManager : MonoBehaviour
    {
        private AudioSource audioSource;
        public static MusicManager Instance { get; private set; }

        private float masterVolume = 1f;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            audioSource = gameObject.GetComponent<AudioSource>();
        }

        public void Volume(float volume)
        {
            audioSource.volume = volume * masterVolume;
        }

        public void MasterVolume(float volume)
        {
            masterVolume = volume;
        }

        public void Pause()
        {
            audioSource.Pause();
        }

        public void Play()
        {
            audioSource.Play();
        }

        public static void SetVolume(float volume)
        {
            if (Instance == null)
            {
                Debug.LogWarning("MusicManager not initialized.");
                return;
            }

            Instance.Volume(volume);
        }

        public static void SetMasterVolume(float masterVolume)
        {
            if (Instance == null)
            {
                Debug.LogWarning("MusicManager not initialized.");
                return;
            }

            Instance.MasterVolume(masterVolume);
        }

        public static void PauseMusic()
        {
            if (Instance == null)
            {
                Debug.LogWarning("MusicManager not initialized.");
                return;
            }

            Instance.Pause();
        }

        public static void PlayMusic()
        {
            if (Instance == null)
            {
                Debug.LogWarning("MusicManager not initialized.");
                return;
            }

            Instance.Play();
        }
    }
}