public class PuzzleResetZone : PuzzleZone
{
    protected override void ZoneInteraction() => PuzzleZoneConnector.ResetConnections();
}
