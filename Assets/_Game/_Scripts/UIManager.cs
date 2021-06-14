using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class UIManager
{
   [SerializeField] Slider healthBar;
   [SerializeField] TextMeshProUGUI timerText;
   [SerializeField] RectTransform hostageKilledText;

   [Header("Weapon HUD")]
   [SerializeField] Image weaponIcon;
   [SerializeField] TextMeshProUGUI ammoText;
   [SerializeField] GameObject reloadWarning;
   private WeaponData currentWeapon;
   public void Init(float maxHealth)
   {
       healthBar.maxValue = maxHealth;
       healthBar.value = maxHealth;
       hostageKilledText.gameObject.SetActive(false);
       PlayerScript.OnWeaponChanged += UpdateWeapon;
       TimerObject.OnTimerChanged += UpdateTimer;
   }

    private void UpdateTimer(int currentTimer)
    {
        timerText.SetText(currentTimer.ToString("00"));
    }

    public void RemoveEvent()
    {
        PlayerScript.OnWeaponChanged -= UpdateWeapon;  
        TimerObject.OnTimerChanged -= UpdateTimer;  
        currentWeapon.OnWeaponFired -= UpdateAmmo;
    }

    public void UpdateWeapon(WeaponData obj)
    {
        if (currentWeapon != null)
            currentWeapon.OnWeaponFired -= UpdateAmmo;
            
        currentWeapon = obj;
        currentWeapon.OnWeaponFired += UpdateAmmo; //Change Bullet Count  
        weaponIcon.sprite = currentWeapon.GetIcon; //Change Weapon Icon
    }

   public void UpdateHealth(float value)
   {
       healthBar.value = value;
   }

   void UpdateAmmo(int ammo)
   {
       //enable reload warning when ammo is 0
       reloadWarning.SetActive(ammo <= 0);
       //format bullet count
       ammoText.SetText(ammo.ToString("00"));
   }

   public void ShowHostageKilled(Vector3 pos, bool show)
   {
       hostageKilledText.gameObject.SetActive(show);

       if(!show)
        return;

     hostageKilledText.position = pos;
     Vector2 adjustPos = Extensions.GetPositionInsideScreen(new Vector2 (1920f, 1080f), hostageKilledText, 25f); 
     //apply to anchored position
     hostageKilledText.anchoredPosition = adjustPos;
   }
}
