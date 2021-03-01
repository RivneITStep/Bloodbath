using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class EnemyLogic : MonoBehaviour
{

    bool isFacingRight = true;

    bool itemFacingRight = true;

    // For items

    public float Distance = 0.5f;
    public bool itemInHand = false;
    //public RaycastHit2D hit;
    public Transform handPoint;
    public GameObject item;

    Rigidbody2D rb;

    private int healthPoints = 100;
    private int armor = 5;

    public GameObject healthBarObj;
    Vector3 localScale;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        localScale = healthBarObj.transform.localScale;
    }


    void Update()
    {
        localScale.x = (healthPoints * 1.5f) / 100;
        healthBarObj.transform.localScale = localScale;
        if (healthPoints <= 0)
        {
            Destroy(gameObject);
        }


    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            healthPoints -= (int)collision.gameObject.GetComponent<Bullet>().damage;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            healthPoints -= (int)collision.gameObject.GetComponent<Bullet>().damage;
        }
    }
}



