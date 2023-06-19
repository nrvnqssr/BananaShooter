using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponActivator : MonoBehaviour
{
    public bool Equip;
    public bool Dequip;
    public GameObject Weapon;

    void OnTriggerEnter(Collider other)
    {
        if (Equip)
        {
            Weapon.SetActive(true);
        }

        if (Dequip)
        {
            Weapon.SetActive(false);
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (Equip)
        {
            Weapon.SetActive(false);
        }

        if (Dequip)
        {
            Weapon.SetActive(true);
        }
    }
}
