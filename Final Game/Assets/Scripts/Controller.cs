using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Final.Projectile;
using System;

public class Controller : MonoBehaviour
{
    [SerializeField] GameObject cameraGameobject = null;
    [SerializeField] GameObject aimPoint = null;
    [SerializeField] Projectile bullet = null;

    EquipWeapon weapon;
    Mover mover;

    //variable to keep track of whether the player is crouched 
    bool crouched = false;
    

    // Start is called before the first frame update
    void Start()
    {
        weapon = gameObject.GetComponent<EquipWeapon>();
        mover = gameObject.GetComponent<Mover>();
    }

    // Update is called once per frame
    void Update()
    {
        if (weapon.isWeaponEquipped())
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot(cameraGameobject.transform, aimPoint.transform);
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                mover.MoveToCursor(crouched);
            }
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(crouched);
            if (crouched == false)
            {
                Crouch();
            } else
            {
                Uncrouch();
            }
        }
    }

    private void Uncrouch()
    {
        crouched = false;
        mover.Uncrouch();
        Deer[] allDeer = FindObjectsOfType<Deer>();
        foreach (Deer thisDeer in allDeer)
        {
            thisDeer.ChangeDetectionDistance(false);
        }
    }

    private void Crouch()
    {
        crouched = true;
        mover.Crouch();
        Deer[] allDeer = FindObjectsOfType<Deer>();
        foreach (Deer thisDeer in allDeer)
        {
            thisDeer.ChangeDetectionDistance(true);
        }
    }

    public bool IsCrouched()
    {
        return crouched;
    }

    public void Shoot(Transform gun, Transform aimPoint)
    {
        Projectile projectileInstance = Instantiate(bullet, gun.position, Quaternion.identity);
        projectileInstance.SetTarget(aimPoint);
    }
}
