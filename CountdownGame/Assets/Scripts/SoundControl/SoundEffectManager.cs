using System.Collections.Generic;
using UnityEngine;

namespace SoundControl
{
    public class SoundEffectManager : MonoBehaviour
    {
        private AudioSource audioSource;
        public static SoundEffectManager Instance { get; private set; }
        private float masterVolume = 1f;
        private float soundEffectsVolume = 1f;

        [Header("SFX List")]
        [SerializeField] private AudioClip notification;

        [SerializeField] private List<AudioClip> soundEffects;

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

        public void PlayAudioClip(AudioClip clip, float pitch = 1f)
        {
            audioSource.pitch = pitch; // For pitch shifting stuff
            audioSource.volume = soundEffectsVolume * masterVolume;
            audioSource.PlayOneShot(clip);
        }

        public void Volume(float volume)
        {
            soundEffectsVolume = volume;
        }

        public void MasterVolume(float volume)
        {
            masterVolume = volume;
        }

        public static void SetMasterVolume(float masterVolume)
        {
            if (Instance == null)
            {
                Debug.LogWarning("SoundEffectsManager not initialized.");
                return;
            }

            Instance.MasterVolume(masterVolume);
        }

        public static void SetVolume(float volume)
        {
            if (Instance == null)
            {
                Debug.LogWarning("SoundEffectsManager not initialized.");
                return;
            }

            Instance.Volume(volume);
        }

        public static void Play(AudioClip clip, float pitch = 1f)
        {

            if (Instance == null)
            {
                Debug.LogWarning("SoundEffectsManager not initialized.");
                return;
            }

            Instance.PlayAudioClip(clip, pitch);
        }

        public static void Play(string name, float pitch = 1f)
        {
            if (Instance == null)
            {
                Debug.LogWarning("SoundEffectManager not initialized.");
                return;
            }

            AudioClip clip = Instance.soundEffects.Find(x => x.name == name);

            if (clip == null)
            {
                Debug.LogWarning($"SFX {name} not found.");
                return;
            }

            Instance.PlayAudioClip(clip, pitch);
        }

    }
}