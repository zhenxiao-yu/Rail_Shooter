using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Build Table For Custom Weapons
[CreateAssetMenu(fileName = "CustomWeaponData", menuName = "Weapon Data")]
public class WeaponData : ScriptableObject
{
    
}


//enumeration for fire modes
public enum FireTypes
{
    SINGlE,
    RAPID
}