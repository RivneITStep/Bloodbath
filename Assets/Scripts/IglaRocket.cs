using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IglaRocket : MonoBehaviour
{
    


    public float damage;

    public float speed;

    public float destroyTime;

    public GameObject target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {

            var distance = Mathf.Abs(target.transform.position.x - transform.position.x);
            var height = Mathf.Abs(target.transform.position.y - transform.position.y);
            transform.rotation = Quaternion.Euler(0,0,(Mathf.Atan(height/distance)/(Mathf.PI/180)));
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
    }
}
