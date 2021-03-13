using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour,IVehicle
{


    public int healthPoints;
    public GameObject GetGameObject()
    {
        return gameObject;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            healthPoints -= (int)collision.gameObject.GetComponent<IBullet>().GetDamage();
            collision.gameObject.GetComponent<IBullet>().DestroyBullet();
        }
    }
}
