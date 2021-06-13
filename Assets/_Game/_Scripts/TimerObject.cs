using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerObject  
{
    public int displayTimer;
    private Coroutine timer;
    public void StartTimer(MonoBehaviour mb, float duration)
    {
        if(timer != null)
        {
            Debug.Log("Timer Already Runs");
            return;
        }
    }

    public void StopTimer(MonoBehaviour mb)
    {
        if (timer == null)
        {
            Debug.Log("There Are No Timer Currently Running");
            return;
        }

        mb.StopCoroutine(timer);
        timer = null;
    }

    IEnumerator TimerRuns(float duration)
    {
        while(duration > 0f)
        {
            displayTimer = (int) duration;
            duration -= 1f;
            yield return new WaitForSeconds(1f);
        }

        timer = null;
    }
}
