using UnityEngine;

public class PuzzleAudioHint : MonoBehaviour
{
    public void StartHintCue(PuzzleZone partnerZone)
    {
        var hint = partnerZone.Cue;

        if (hint == null) return;
        hint.StartEmitting(partnerZone.gameObject);
    }

    public void StopHintCue(PuzzleZone partnerZone)
    {
        var hint = partnerZone.Cue;

        if (hint == null) return;
        hint.StopEmitting();
    }
}
