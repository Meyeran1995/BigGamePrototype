using System.Linq;

namespace Mushroomancer.Audio
{
    public enum AudioEvent
    {
        None,
        OnEnable,
        OnDisable,
        Start,
        OnDestroy,
        OnCollisionEnter,
        OnCollisionEnter2D,
        OnCollisionExit,
        OnCollisionExit2D,
        OnTriggerEnter,
        OnTriggerEnter2D,
        OnTriggerExit,
        OnTriggerExit2D
    }

    public enum AudioCollisionEventType
    {
        None,
        Collision,
        Trigger,
    }

    public static class AudioUtilities
    {
        public static AudioCollisionEventType GetCollisionEventType(AudioEvent activeAudioEvent)
        {
            var collisionEventType = AudioCollisionEventType.None;

            if (activeAudioEvent == AudioEvent.None) return collisionEventType;

            AudioEvent[] colAudioEvents = {
                AudioEvent.OnCollisionEnter,
                AudioEvent.OnCollisionEnter2D,
                AudioEvent.OnCollisionExit,
                AudioEvent.OnCollisionExit2D,
                AudioEvent.OnTriggerEnter,
                AudioEvent.OnTriggerEnter2D,
                AudioEvent.OnTriggerExit,
                AudioEvent.OnTriggerExit2D
            };

            if (!colAudioEvents.Contains(activeAudioEvent)) return collisionEventType;

            for (int i = 0; i < colAudioEvents.Length; i++)
            {
                if (activeAudioEvent != colAudioEvents[i]) continue;

                collisionEventType = i > 3 ? AudioCollisionEventType.Trigger : AudioCollisionEventType.Collision;
            }

            return collisionEventType;
        }

        public static bool HasMatchingColliderType(AudioEvent activeAudioEvent, bool is3D)
        {
            if (is3D)
            {
                AudioEvent[] colAudioEvents3D = {
                    AudioEvent.OnCollisionEnter,
                    AudioEvent.OnCollisionExit,
                    AudioEvent.OnTriggerEnter,
                    AudioEvent.OnTriggerExit
                };

                return colAudioEvents3D.Contains(activeAudioEvent);
            }
            else
            {
                AudioEvent[] colAudioEvents2D = {
                    AudioEvent.OnCollisionEnter2D,
                    AudioEvent.OnCollisionExit2D,
                    AudioEvent.OnTriggerEnter2D,
                    AudioEvent.OnTriggerExit2D
                };

                return colAudioEvents2D.Contains(activeAudioEvent);
            }
        }
    }
}