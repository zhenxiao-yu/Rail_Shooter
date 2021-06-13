using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class UIManager
{
   [SerializeField] Slider healthBar;
   public void Init(float maxHealth)
   {
       healthBar.maxValue = maxHealth;
       healthBar.value = maxHealth;
   }

   public void UpdateHealth(float value)
   {
       healthBar.value = value;
   }
}
