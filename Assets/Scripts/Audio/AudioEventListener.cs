using UnityEngine;

namespace Mushroomancer.Audio
{
    public class AudioEventListener : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private Sound sound;
        [Header("Events")]
        [SerializeField] private string collisionTag;
        [SerializeField] private AudioEvent playEvent, stopEvent;

        private void Awake()
        {
            if (!TryGetComponent(out audioSource))
                audioSource = gameObject.AddComponent<AudioSource>();
        }

        private void OnEventTriggered(AudioEvent triggeredEvent)
        {
            if (triggeredEvent == playEvent)
            {
                sound.Play();
                return;
            }

            if (triggeredEvent != stopEvent) return;

            sound.Stop();
        }

        private void Start()
        {
            sound.SetUpSource(audioSource);
            OnEventTriggered(AudioEvent.Start);
        }

        private void OnEnable() => OnEventTriggered(AudioEvent.OnEnable);

        private void OnDisable() => OnEventTriggered(AudioEvent.OnDisable);

        private void OnTriggerEnter(Collider other)
        {
            if (string.IsNullOrEmpty(collisionTag) || other.CompareTag(collisionTag))
            {
                OnEventTriggered(AudioEvent.OnTriggerEnter);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (string.IsNullOrEmpty(collisionTag) || other.CompareTag(collisionTag))
            {
                OnEventTriggered(AudioEvent.OnTriggerExit);
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (string.IsNullOrEmpty(collisionTag) || other.CompareTag(collisionTag))
            {
                OnEventTriggered(AudioEvent.OnTriggerEnter2D);
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (string.IsNullOrEmpty(collisionTag) || other.CompareTag(collisionTag))
            {
                OnEventTriggered(AudioEvent.OnTriggerExit2D);
            }
        }

        private void OnCollisionEnter() => OnEventTriggered(AudioEvent.OnCollisionEnter);

        private void OnCollisionExit() => OnEventTriggered(AudioEvent.OnCollisionExit);

        private void OnCollisionEnter2D() => OnEventTriggered(AudioEvent.OnCollisionEnter2D);

        private void OnCollisionExit2D() => OnEventTriggered(AudioEvent.OnCollisionExit2D);

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (playEvent == AudioEvent.None && stopEvent == AudioEvent.None) return;

            AudioCollisionEventType playEventType = AudioUtilities.GetCollisionEventType(playEvent);
            AudioCollisionEventType stopEventType = AudioUtilities.GetCollisionEventType(stopEvent);

            if (playEventType == AudioCollisionEventType.None && stopEventType == AudioCollisionEventType.None) return;

            if (!CheckCollisionSetUp(playEvent, out Collider col3D, out Collider2D col2D) &&
                !CheckCollisionSetUp(stopEvent, out col3D, out col2D)) return;

            if (col3D)
            {
                col3D.isTrigger = playEventType == AudioCollisionEventType.Trigger || stopEventType == AudioCollisionEventType.Trigger;
            }
            else
            {
                col2D.isTrigger = playEventType == AudioCollisionEventType.Trigger || stopEventType == AudioCollisionEventType.Trigger;
            }
        }

        private bool CheckCollisionSetUp(AudioEvent collisionEvent, out Collider col3D, out Collider2D col2D)
        {
            col3D = GetComponent<Collider>();
            col2D = GetComponent<Collider2D>();

            if (collisionEvent == AudioEvent.None) return true;
            
            if (!col3D && !col2D)
            {
                Debug.LogWarning($"GameObject {name} is trying to listen for a collision event but has no collider");
                return false;
            }

            if (AudioUtilities.HasMatchingColliderType(collisionEvent, col3D != null)) return true;

            Debug.LogWarning($"Collider and AudioEvent type do not match on GameObject {name}");
            return false;
        }
#endif
    }
}
