using UnityEngine;

public class PlayerPuzzleTrail : MonoBehaviour
{
   [SerializeField] private Transform player;
   [SerializeField] private LineRenderer playertrailRenderer;
   [SerializeField] private Material lineMaterial;
   private PuzzleZone connectedZone;

   private void Awake()
   {
      GameObject trail = new GameObject("PlayerTrailRendObject", typeof(LineRenderer));
      trail.transform.SetParent(transform);
      playertrailRenderer = trail.GetComponent<LineRenderer>();
      playertrailRenderer.sharedMaterial = lineMaterial;
      playertrailRenderer.gameObject.SetActive(false);
   }

   private void FixedUpdate()
   {
      if(connectedZone == null || !player.hasChanged) return;
      UpdatePos();
   }

   private void UpdatePos() => playertrailRenderer.SetPositions(new []{player.position, connectedZone.transform.position});

   public void ConnectToPlayer(PuzzleZone zone)
   {
      connectedZone = zone;
      playertrailRenderer.gameObject.SetActive(true);
   }

   public void Disconnect()
   {
      connectedZone = null;
      playertrailRenderer.gameObject.SetActive(false);
   }
}
