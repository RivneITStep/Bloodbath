﻿using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour,IWeapon
{

    public Transform bulletStartPoint;

    public float Damage;

    public GameObject bullet;

    private float shotTime;

    public float startTime;

    public bool isOnPlayerHand = false;


    public float reloadTime;
    public float currReloadTime;

    public bool isReloading = false;


    public float BulletSpeed;

    public float BulletDestroyTime;

    public int ammo;

    public int maxAmmoInMagazine;

    public int currAmmo;

    public float recoil;

    public float distance;

    public bool isFacingRight = true;

    void Start()
    {
        bullet.GetComponent<Bullet>().destroyTime = BulletDestroyTime;
        bullet.GetComponent<Bullet>().speed = BulletSpeed;

        bullet.GetComponent<Bullet>().damage = Damage;
    }



    float Distance(float x1, float y1, float z1,float x2, float y2,float z2)
    {
        return Mathf.Sqrt(Mathf.Pow((x1-x2),2)+ Mathf.Pow((y1 - y2), 2)+ Mathf.Pow((z1 - z2), 2));
    }
    // Update is called once per frame
    void Update()
    {
        if (isOnPlayerHand)
        {
            shotTime -= Time.deltaTime;
            currReloadTime -= Time.deltaTime;
            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) /*- transform.position*/;


            if ( /*Mathf.Abs(Mathf.Abs(pos.x) - Mathf.Abs(transform.position.x))*/  
                Distance(pos.x,pos.y,pos.z,transform.position.x, transform.position.y, transform.position.z) < distance) {
             
                Camera.main.transform.position = pos;

            }
            else
            {
                var tmp = pos;
                tmp.x = transform.position.x + ((transform.position.x > pos.x) ? (-1) * distance : distance);
                var res = tmp;

                Camera.main.transform.position = res;
                Input.mousePosition.Set(res.x, res.y, res.z);
            }

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
    }

    public void Fire()
    {
        System.Random rnd = new System.Random();
        if (shotTime <= 0 && currAmmo > 0)
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
           
            float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            var rotation = Quaternion.Euler(0f, 0f, rotateZ); ;
            rotation.z += (recoil / ((shotTime == 0) ? -0.1f : shotTime)) * rnd.Next(-1, 1);
            Instantiate(bullet, bulletStartPoint.position, rotation);
            shotTime = startTime;
            currAmmo--;
            if (currAmmo == 0 && ammo > 0)
            {
                currReloadTime = reloadTime;
                isReloading = !isReloading;
            }
        }
    }

    public int GetAmmo()
    {
        return ammo;
    }

    public int GetCurrAmmo()
    {
        return currAmmo;
    }

    public float GetCurrReloadTime()
    {
        return currReloadTime;
    }

    public bool GetIsReloading()
    {
        return isReloading;
    }

    public float GetReloadTime()
    {
        return reloadTime;
    }

    public void Reload()
    {
        ammo += currAmmo;
        currAmmo = 0;
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
