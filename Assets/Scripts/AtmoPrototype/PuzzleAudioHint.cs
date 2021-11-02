using UnityEngine;

public class PuzzleAudioHint : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event[] hintCues;

    public void StartHintCue(PuzzleZone zone)
    {
        int index = zone.PartnerIndex;
        if(index > hintCues.Length || index == -1) return;

        hintCues[index].Post(zone.Partner.gameObject);
    }
    
    public void StopHintCue(PuzzleZone zone)
    {
        int index = zone.PartnerIndex;
        if(index > hintCues.Length || index == -1) return;

        hintCues[index].Stop(zone.Partner.gameObject);
    }
}
