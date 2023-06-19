using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchDFT : MonoBehaviour
{
    public DynamicTextData data1;
    public DynamicTextData data2;
    public DynamicTextData data3;
    public DynamicTextData data4;
    public DynamicTextData data5;
    public DynamicTextData data6;
    public DynamicTextData data7;
    public DynamicTextData data8;
    public DynamicTextData data9;
    public DynamicTextData data10;
    public DynamicTextData data11;
    public DynamicTextData data12;

    void Update()
    {
        GetInput();
    }
        
    private void GetInput()
    {
        if (Input.GetKeyDown(KeyCode.F1)) Weapon.weapon.data = data1;
        if (Input.GetKeyDown(KeyCode.F2)) Weapon.weapon.data = data2;
        if (Input.GetKeyDown(KeyCode.F3)) Weapon.weapon.data = data3;
        if (Input.GetKeyDown(KeyCode.F4)) Weapon.weapon.data = data4;
        if (Input.GetKeyDown(KeyCode.F5)) Weapon.weapon.data = data5;
        if (Input.GetKeyDown(KeyCode.F6)) Weapon.weapon.data = data6;
        if (Input.GetKeyDown(KeyCode.F7)) Weapon.weapon.data = data7;
        if (Input.GetKeyDown(KeyCode.F8)) Weapon.weapon.data = data8;
        if (Input.GetKeyDown(KeyCode.F9)) Weapon.weapon.data = data9;
        if (Input.GetKeyDown(KeyCode.F10)) Weapon.weapon.data = data10;
        if (Input.GetKeyDown(KeyCode.F11)) Weapon.weapon.data = data11;
        if (Input.GetKeyDown(KeyCode.F11)) Weapon.weapon.data = data12;
    }
}
