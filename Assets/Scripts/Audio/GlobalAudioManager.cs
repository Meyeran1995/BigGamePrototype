using UnityEngine;

namespace Mushroomancer.Audio
{
    public class GlobalAudioManager : MonoBehaviour
    {
        public static GlobalAudioManager Instance { get; private set; }

        [SerializeField] private Sound[] globalSounds;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            DontDestroyOnLoad(gameObject);

            for (int i = 0; i < globalSounds.Length; i++)
            {
                GameObject _go = new GameObject("Sound_" + i + "_" + globalSounds[i].SoundName);
                _go.transform.SetParent(this.transform);
                globalSounds[i].SetUpSource(_go.AddComponent<AudioSource>());
            }
        }

        public void PlaySound(string pSoundName)
        {
            for (int i = 0; i < globalSounds.Length; i++)
            {
                if (globalSounds[i].SoundName != pSoundName) continue;
                globalSounds[i].Play();
                return;
            }

            Debug.LogWarning("AudioManager: Sound not found in list, " + pSoundName);
        }

        public void StopPlaying(string pSoundName)
        {
            for (int i = 0; i < globalSounds.Length; i++)
            {
                if (globalSounds[i].SoundName != pSoundName) continue;
                globalSounds[i].Stop();
                return;
            }

            Debug.LogWarning("AudioManager: Sound not found in list, " + pSoundName);
        }

        public void StopPlayingAll()
        {
            foreach (var s in globalSounds)
                s.Stop();
        }
    }
}
