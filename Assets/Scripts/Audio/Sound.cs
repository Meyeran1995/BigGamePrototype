using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Mushroomancer.Audio
{
    [Serializable]
    public class Sound
    {
        public string SoundName => soundName;
        [SerializeField] private string soundName;
        [SerializeField] private AudioClip clip;

        [Range(0f, 1f)] [SerializeField] private float volume = 0.7f;
        [Range(0.5f, 1.5f)] [SerializeField] private float pitch = 1f;
        [SerializeField] private bool loop = false;
        [SerializeField] private AudioMixerGroup mixerGroup;

        private AudioSource source;

        public void SetUpSource(AudioSource pSource)
        {
            source = pSource;
            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;
            source.loop = loop;
            source.outputAudioMixerGroup = mixerGroup;
        }

        public void Play() => source.Play();

        public void Stop() => source.Stop();
    }
}