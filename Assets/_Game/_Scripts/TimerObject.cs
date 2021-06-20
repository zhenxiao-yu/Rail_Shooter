using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TimerObject  
{
    public static System.Action<int> OnTimerChanged = delegate { };

    public int displayTimer;
    private Coroutine timer;
    public void StartTimer(MonoBehaviour mb, float duration)
    {
        if(timer != null)
        {
            return;
        }

        timer = mb.StartCoroutine(TimerRuns(duration));
    }

    public void StopTimer(MonoBehaviour mb)
    {
        if (timer == null)
        {
            return;
        }

        mb.StopCoroutine(timer);
        timer = null;
    }

    IEnumerator TimerRuns(float duration)
    {
        while(duration > 0f)
        {
            if(GameManager.Instance.PlayerDead)
                yield break;

            OnTimerChanged((int)duration);
            displayTimer = (int) duration;
            duration -= 1f;
            yield return new WaitForSeconds(1f);
        }

        timer = null;
    }
}
