using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDataBase : MonoBehaviour
{

    public List<GameObject> Guns = new List<GameObject>();
    
    
    // Start is called before the first frame update



    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetById(string id)
    {
        foreach(var g in Guns)
        {
            if (g.GetComponent<ItemInfo>().item_id == id)
                return g;
        }
        return null;
    }
}
