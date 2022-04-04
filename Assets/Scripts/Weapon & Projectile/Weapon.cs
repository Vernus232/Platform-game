using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public enum WeaponType : int
{
    Burst, Melee
}

public class Weapon : MonoBehaviour
{
    public string weaponName;
    public WeaponType type;
}
