using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour, IHitable
{
    [SerializeField] WeaponData weapon; //Weapon Type

    private PlayerScript player; //Player Ref

    public void Hit(RaycastHit hit, int damage = 1)
    {
        //Switch To Custom Weapon
        player.SwitchWeapon(weapon);
        //Destroy Pickup Object
        Destroy(gameObject);
    }

    void Start()
    {
        player = FindObjectOfType<PlayerScript>();
    }

}
