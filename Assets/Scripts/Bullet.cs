using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour


{


    public float damage;

    public float speed;

    public float destroyTime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyBullet",destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
