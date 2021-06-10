using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//create custom extensions
public static class Extensions 
{
   public static void DelayedAction(this MonoBehaviour mb, System.Action action, float delay)
   {
       mb.StartCoroutine(DelayedCoroutine(action, delay));
   }

   static IEnumerator DelayedCoroutine(System.Action action, float delay)
   {
       yield return new WaitForSeconds(delay);

       action?.Invoke();
   }
}
