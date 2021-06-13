using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

[System.Serializable]
public class UIManager
{
   [SerializeField] Slider healthBar;

   [Header("Weapon HUD")]
   [SerializeField] Image weaponIcon;
   [SerializeField] TextMeshProUGUI ammoText;
   [SerializeField] GameObject reloadWarning;
   private WeaponData currentWeapon;
   public void Init(float maxHealth)
   {
       healthBar.maxValue = maxHealth;
       healthBar.value = maxHealth;
       PlayerScript.OnWeaponChanged += UpdateWeapon;
   }

   public void RemoveEvent()
    {
        PlayerScript.OnWeaponChanged -= UpdateWeapon;    
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
}
