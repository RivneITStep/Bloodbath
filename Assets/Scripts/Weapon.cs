using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Weapon : MonoBehaviour
    {

        public Transform bulletStartPoint;

        public float Damage;

        public GameObject bullet;

        private float shotTime;

        public float startTime;



        public float reloadTime;
        public float currReloadTime;

        public bool isReloading = false;


        public float BulletSpeed;

        public float BulletDestroyTime;

        public int ammo;

        public int maxAmmoInMagazine;

        public int currAmmo;

        public float recoil;



        void Start()
        {
            bullet.GetComponent<Bullet>().destroyTime = BulletDestroyTime;
            bullet.GetComponent<Bullet>().speed = BulletSpeed;

            bullet.GetComponent<Bullet>().damage = Damage;
        }


        public void Fire()
        {
            System.Random rnd = new System.Random();
            if (shotTime <= 0 && currAmmo > 0)
            {
                var rotation = transform.rotation;
                rotation.z += (recoil/((shotTime == 0) ? -0.1f : shotTime)) * rnd.Next(-1,1) ; 
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

        void Update()
        {
            shotTime -= Time.deltaTime;
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

        float ToMin(float number)
        {
            float res;
            int i_num = (int)number;
            int count = 1;
            for(;i_num / 10 >0 ;)
            {
                i_num /= 10;
                count *= 10;
            }
            res = number / count;

            return count;
        }
       
    }
}
