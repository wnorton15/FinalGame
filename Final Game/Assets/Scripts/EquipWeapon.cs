using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipWeapon : MonoBehaviour
{
    [SerializeField] GameObject weapon = null;
    bool weaponEquipped = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon == null)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (weaponEquipped)
            {
                Unequip();
            } else
            {
                Equip();
            }
        }
    }

    private void Equip()
    {
        weapon.SetActive(true);
        weaponEquipped = true;
    }

    private void Unequip()
    {
        weapon.SetActive(false);
        weaponEquipped = false;
    }

    public bool isWeaponEquipped()
    {
        return weaponEquipped;
    }
}
