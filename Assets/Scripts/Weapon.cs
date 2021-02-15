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


        public float BulletSpeed;

        public float BulletDestroyTime;



        void Start()
        {
            bullet.GetComponent<Bullet>().destroyTime = BulletDestroyTime;
            bullet.GetComponent<Bullet>().speed = BulletSpeed;

            bullet.GetComponent<Bullet>().damage = Damage;
        }


        public void Fire()
        {
            if (shotTime <= 0)
            {
                Instantiate(bullet, bulletStartPoint.position, transform.rotation);
                shotTime = startTime;
            }
        }

        void Update()
        {
            shotTime -= Time.deltaTime;
        }
       
    }
}
