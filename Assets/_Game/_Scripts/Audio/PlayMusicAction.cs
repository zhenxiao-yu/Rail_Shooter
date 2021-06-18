using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusicAction : MonoBehaviour
{
   [SerializeField] AudioGetter audioSfx;
   [SerializeField] float delay;

   private void OnEnable()
   {
       this.DelayedAction(delegate
       {
           AudioPlayer.Instance.PlayMusic(audioSfx);
       }, delay);
   }
}
