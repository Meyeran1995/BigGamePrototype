using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class EventLooper : MonoBehaviour
{
   [SerializeField] private float loopInterval;
   [SerializeField] private AK.Wwise.Event eventToLoop;
   private Coroutine loopRoutine;

   private void Start()
   {
      loopRoutine = StartCoroutine(Loop());
   }

   private IEnumerator Loop()
   {
      yield return new WaitForSeconds(loopInterval);

      eventToLoop.Post(gameObject);

      loopRoutine = StartCoroutine(Loop());
   }

   private void OnTriggerExit(Collider other)
   {
      StopAllCoroutines();
      loopRoutine = null;
   }

   private void OnTriggerEnter(Collider other)
   {
      if (loopRoutine != null) return;
      // {
      //    Debug.Log("reentered but coroutine is there");
      //    return;
      // }
      loopRoutine = StartCoroutine(Loop());
   }
}
