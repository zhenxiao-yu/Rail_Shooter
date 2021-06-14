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

   public static Vector3 GetPositionInsideScreen(Vector2 baseRes, RectTransform rect, float offset)
   {
       float widthBounds = baseRes.x - rect.rect.width - offset;
       float heightBounds = baseRes.y - rect.rect.height - offset;

       Vector2 adjustedPos = rect.anchoredPosition;
        //position for the "hostage killed" text
       adjustedPos.x = Mathf.Clamp(adjustedPos.x, widthBounds * -0.5f, widthBounds * 0.5f);
       adjustedPos.y = Mathf.Clamp(adjustedPos.y, heightBounds * -0.5f, heightBounds * 0.5f);

       return adjustedPos;
   }
}
