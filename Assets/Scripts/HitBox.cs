using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public Enemy enemy;

    public void OnRaycastHit(Weapon weapon)
    {
        enemy.Damage((int)weapon.damage);   
    }
}