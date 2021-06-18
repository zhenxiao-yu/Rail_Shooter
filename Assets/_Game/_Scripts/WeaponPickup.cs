using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour, IHitable
{
    [SerializeField] WeaponData weapon; //Weapon Type
    [SerializeField] float rotateSpeed = 90f; //How Fast The Pickup Roatates
    [SerializeField] AudioGetter pickupSfx;

    private PlayerScript player; //Player Ref

    public void Hit(RaycastHit hit, int damage = 1)
    {
        //Switch To Custom Weapon
        player.SwitchWeapon(weapon);
        AudioPlayer.Instance.PlaySFX(pickupSfx);
        //Destroy Pickup Object
        Destroy(gameObject);
    }

    void Start()
    {
        player = FindObjectOfType<PlayerScript>();
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

}
