using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketLauncher : MonoBehaviour,IWeapon
{
    public Transform bulletStartPoint;

    public float Damage;

    public GameObject bullet;

  

    public bool isOnPlayerHand = false;

    public float reloadTime;
    public float currReloadTime;

    public bool isReloading = false;


    public float BulletSpeed;

    public float BulletDestroyTime;

    public int ammo;

    public int maxAmmoInMagazine;

    public int currAmmo;


    public bool isFacingRight = true;
    void Start()
    {
        bullet.GetComponent<Rocket>().destroyTime = BulletDestroyTime;
        bullet.GetComponent<Rocket>().speed = BulletSpeed;

        bullet.GetComponent<Rocket>().damage = Damage;
    }

    public void Fire()
    {




        
        if (currAmmo > 0)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(0f, 0f, rotateZ); 
            Instantiate(bullet, bulletStartPoint.position, rotation);

            currAmmo--;
            if (currAmmo == 0 && ammo > 0)
            {
                currReloadTime = reloadTime;
                isReloading = !isReloading;
            }
        }



    }

    void Update()
    {

        currReloadTime -= Time.deltaTime;



        if (isReloading && currReloadTime <= 0)
        {
            if (ammo > 0 && (ammo - maxAmmoInMagazine) > 0)
            {
                ammo -= maxAmmoInMagazine;
                currAmmo = maxAmmoInMagazine;
            }
            else
            {
                if (ammo > 0)
                {
                    currAmmo = ammo;
                    ammo = 0;
                }
            }
            isReloading = !isReloading;
        }
    }



    public void Reload()
    {
        ammo += currAmmo;
        currAmmo = 0;

    }




    public int GetCurrAmmo()
    {
        return currAmmo;
    }

    public int GetAmmo()
    {
        return ammo;
    }

    public bool GetIsReloading()
    {
        return isReloading;
    }

    public float GetCurrReloadTime()
    {
        return currReloadTime;
    }

    public float GetReloadTime()
    {
        return reloadTime;
    }

    public bool GetIsFacingRight()
    {
        return isFacingRight;
    }

    public void SetIsFacingRight(bool value)
    {
        isFacingRight = value;
    }

    public void SetIsOnPlayerHand(bool value)
    {
        isOnPlayerHand = value;
    }
}
