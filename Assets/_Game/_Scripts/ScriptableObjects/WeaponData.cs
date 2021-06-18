using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Build Table For Custom Weapons
[CreateAssetMenu(fileName = "CustomWeaponData", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{   
    //Bullet Amount Change Event
    public System.Action<int> OnWeaponFired = delegate {};
    [SerializeField] FireTypes type; //Fire Mode
    [SerializeField] float rate = 0.15f; //Fire Rate
    [SerializeField] int maxAmmo; //Ammo Capacity
    [SerializeField] int damageValue; //Damage Value
    [SerializeField] bool defaultWeapon; //Is Default Weapon?
    [SerializeField] GameObject muzzleFX; //muzzle effect
    [SerializeField] AudioGetter gunShotSfx; //Shooting Sound
    [SerializeField] float fxScale = 0.1f; //size of effect
    [SerializeField] Sprite weaponIcon;

    private Camera cam; //Camera Ref
    private ParticleSystem cachedFX;
    private PlayerScript player;
    private int currentAmmo;
    private float nextFireTime;

    public Sprite GetIcon { get => weaponIcon; } // change Weapon Icon

    public void SetupWeapon(Camera cam, PlayerScript player)
    {
        this.cam = cam;
        this.player = player;
        nextFireTime = 0f; 
        currentAmmo = maxAmmo;
        OnWeaponFired(currentAmmo);

        if(muzzleFX != null)
        {
            GameObject temp = Instantiate(muzzleFX);
            temp.transform.localScale = Vector3.one * fxScale;
            player.SetMuzzleFx(temp.transform);
            cachedFX = temp.GetComponent<ParticleSystem>();
        }
    }

    public void WeaponUpdate()
    {   
        //Check Fire Mode
        if (type == FireTypes.SINGlE)
        {
            if (Input.GetMouseButtonDown(0) && currentAmmo > 0) //left click
            {
                Fire();
                currentAmmo--;
                OnWeaponFired(currentAmmo);
            }
            else
            {
                //ammo runs out
            }
        }
        else
        {
            if (Input.GetMouseButton(0) && Time.time > nextFireTime && currentAmmo > 0) //left hold
            {
                Fire();
                OnWeaponFired(currentAmmo);
                currentAmmo--;
                nextFireTime = Time.time + rate;
            }
            else if (currentAmmo <= 0)
            {
                //ammo runs out
            } 
        }

        if (defaultWeapon && Input.GetMouseButtonDown(1)) //Reload Using Right Click For Custom Weapon
        {
            currentAmmo = maxAmmo;
            OnWeaponFired(currentAmmo);
        }

        if (!defaultWeapon && currentAmmo <= 0)
        {
            //if run out weapon, switch back to default weapon
            player.SwitchWeapon();
        }
    }



    private void Fire()
    {
        AudioPlayer.Instance.PlaySFX(gunShotSfx.audioName, player.transform); //play gunshot sound
        
        RaycastHit hit;
        Ray ray = cam.ScreenPointToRay(Input.mousePosition); //cast ray based on position of mouse

        if (cachedFX != null)
        {
            Vector3 muzzlePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0.2f)); //Set Muzzle position
            cachedFX.transform.position = muzzlePos;
            cachedFX.transform.rotation = Quaternion.LookRotation(ray.direction);
            cachedFX.Play();
        }
        

        if (Physics.Raycast(ray, out hit, 50f)) //50f = max distance
        {
            if (hit.collider != null)
            {
                //Make hitables an array for objects with multiple on hit components
                IHitable[] hitables = hit.collider.GetComponents<IHitable>();
                //Check calidity of hitable objects and excute hit
                if (hitables != null && hitables.Length > 0)
                {
                    foreach (var hitable in hitables)
                    {
                        hitable.Hit(hit, damageValue); //apply damage

                        if (hitable is EnemyScript)
                        {
                            GameManager.Instance.ShotHit(true);
                            return;
                        }
                        else
                        {
                            GameManager.Instance.ShotHit(false);
                        }
                    }
                }
                Debug.Log(hit.collider.gameObject.name); //check collision target name
            }
            return;
        }
        GameManager.Instance.ShotHit(false);
    }
}


//enumeration for fire modes
public enum FireTypes
{
    SINGlE,
    RAPID
}