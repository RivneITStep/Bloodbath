using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour, IBullet
{
    // Start is called before the first frame update

    public float damage;

    public float speed;

    public float destroyTime;

    public GameObject explosionParticlesObject;

    public void DestroyBullet()
    {
        Explose();
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }

    void Start()
    {
        Invoke("DestroyBullet", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    public void Explose()
    {

        var r = Instantiate(explosionParticlesObject, transform.position, Quaternion.Euler(0, 0, 0));
        r.GetComponent<ParticleSystem>().Play(true);





    }
}

