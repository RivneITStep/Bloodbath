using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IglaRocket : MonoBehaviour,IBullet
{
    


    public float damage;

    public float speed;

    public float destroyTime;

    public GameObject target;

    private bool isExplosing = false;

    public float distanceBeforeFlyUp;

    public bool goDown = false;

    public Igla igla;

    public GameObject explosionParticlesObject;


    void Start()
    {
        Invoke("DestroyRocket", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isExplosing)
        {
            if (target != null)
            {

                if (goDown)
                {

                    var distance = Mathf.Abs(target.transform.position.x - transform.position.x);
                    var height = target.transform.position.y - transform.position.y/**((target.transform.position.y < transform.position.y) ? -1 : 1)*/;
                    transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan(height / distance) / (Mathf.PI / 180)));
                    transform.Translate(Vector2.right * speed * 3f * Time.deltaTime);
                }
                else if (Mathf.Abs(target.transform.position.x - transform.position.x) <= distanceBeforeFlyUp)
                {
                    if (transform.rotation.z != 90f)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, 90);
                    }
                    transform.Translate(Vector2.right * speed * 2f * Time.deltaTime);
                    if (Mathf.Abs(transform.position.y - target.transform.position.y) >= distanceBeforeFlyUp * 5)
                        goDown = true;
                }
                else
                {
                    var distance = Mathf.Abs(target.transform.position.x - transform.position.x);
                    var height = target.transform.position.y - transform.position.y/**((target.transform.position.y < transform.position.y) ? -1 : 1)*/;
                    transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan(height / distance) / (Mathf.PI / 180)));
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                }
                
            }
        }
    }

    void DestroyRocket()
    {
        Explose();
       Destroy(gameObject);
        igla.isTargetLocked = false;
        foreach (var o in igla.showTrigeringObj)
        {
            Destroy(o);
        }
    }
    public float GetDamage()
    {
        return damage;
    }

    public void Explose()
    {

        var r = Instantiate(explosionParticlesObject, transform.position, Quaternion.Euler(0, 0, 0));
        r.GetComponent<ParticleSystem>().Play(true);
        
        
        


    }

    public void DestroyBullet()
    {
        DestroyRocket();
    }
}
