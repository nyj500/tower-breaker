using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerBreaker.Core
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        [SerializeField] private AudioSource sfxSource;
        [SerializeField] private AudioSource hitSfxSource;
        [SerializeField] private AudioSource bgmSource;

        [Header("Volume")]
        [SerializeField, Range(0f, 1f)] private float sfxVolume = 1f;
        [SerializeField, Range(0f, 1f)] private float bgmVolume = 0.5f;

        [Header("BGM per Scene")]
        [SerializeField] private SceneBGMEntry[] sceneBGMs;

        [System.Serializable]
        private struct SceneBGMEntry
        {
            public string sceneName;
            public AudioClip bgmClip;
        }

        private void Awake()
        {
            if (Instance != null && Instance != this) { Destroy(gameObject); return; }
            Instance = this;
            DontDestroyOnLoad(gameObject);

            sfxSource.volume = sfxVolume;
            if (hitSfxSource != null) hitSfxSource.volume = sfxVolume;
            bgmSource.volume = bgmVolume;

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            foreach (var entry in sceneBGMs)
            {
                if (entry.sceneName == scene.name)
                {
                    PlayBGM(entry.bgmClip);
                    return;
                }
            }
        }

        public void PlaySFX(AudioClip clip, float volumeScale = 1f)
        {
            if (clip == null) return;
            sfxSource.PlayOneShot(clip, volumeScale);
        }

        public void PlayHitSFX(AudioClip clip)
        {
            if (clip == null) return;
            var source = hitSfxSource != null ? hitSfxSource : sfxSource;
            source.PlayOneShot(clip);
        }

        public void PlayBGM(AudioClip clip)
        {
            if (clip == null) return;
            if (bgmSource.clip == clip && bgmSource.isPlaying) return;
            bgmSource.clip = clip;
            bgmSource.loop = true;
            bgmSource.Play();
        }

        public void StopBGM() => bgmSource.Stop();
    }
}
