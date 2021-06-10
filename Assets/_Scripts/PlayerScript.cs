using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [SerializeField] WeaponData defaultWeapon;

    private Camera cam; //Main Camera Ref
    private WeaponData currentWeapon;
    private Transform childFx;

    void Start()
    {
        this.DelayedAction(delegate { Debug.Log("Delayed Action Ran After 5"); }, 5f );
        cam = GetComponent<Camera>(); //Get Camera Ref

        SwitchWeapon();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentWeapon != null)
            currentWeapon.WeaponUpdate();
    }

    public void SwitchWeapon(WeaponData weapon = null)
    {
        currentWeapon = weapon != null? weapon: defaultWeapon; //if no weapon, switch to default weapon
        currentWeapon.SetupWeapon(cam, this);
    }

    public void SetMuzzleFx(Transform fx)
    {
        if (childFx != null)
            Destroy(childFx.gameObject);

        fx.SetParent(transform);
        childFx = fx;
    }
}
