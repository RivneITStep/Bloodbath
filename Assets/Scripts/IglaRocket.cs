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

                var distance = Mathf.Abs(target.transform.position.x - transform.position.x);
                var height = target.transform.position.y - transform.position.y/**((target.transform.position.y < transform.position.y) ? -1 : 1)*/;
                transform.rotation = Quaternion.Euler(0, 0, (Mathf.Atan(height / distance) / (Mathf.PI / 180)));
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                if(transform.position.x == target.transform.position.x &&
                    transform.position.y == target.transform.position.y)
                {
                    isExplosing = !isExplosing;
                    DestroyRocket();
                }
            }
        }
    }

    void DestroyRocket()
    {
        Explose();
       // Destroy(gameObject);
    }
    public float GetDamage()
    {
        return damage;
    }

    public void Explose()
    {
       
        var system =  GetComponent<ParticleSystem>();
        var shape = system.shape;
        shape.shapeType = ParticleSystemShapeType.Circle;
        shape.radius = 1f;
        system.startLifetime = 2f;
        system.loop = false;
        
        


    }

}
