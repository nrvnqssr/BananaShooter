using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stat : MonoBehaviour
{
    public TextMeshProUGUI valueText;
    public TextMeshProUGUI costText;
    public float cost = 100;
    public float value = 1;
    public float costMultiplier = 1.1f;
    public float valueMultiplier = 1.1f;
    public float valueFlat= 1f;
    private bool spent;

    public void UpgradeStat()
    {
        Spend(cost);
        if (spent)
        {
            Upgrade();
        }
    }

    protected void Spend(float cost)
    {
        if (Weapon.score - (int)cost >= 0)
        {
            Weapon.score -= (int)cost;
            spent = true;
        }
        else
            spent = false;
    }

    protected void Upgrade()
    {
        value = value * valueMultiplier + valueFlat;
        cost *= costMultiplier;
        valueText.text = "Значение: " + ((int)value).ToString();
        costText.text = "Стоимость: " + ((int)cost).ToString();
    }
}