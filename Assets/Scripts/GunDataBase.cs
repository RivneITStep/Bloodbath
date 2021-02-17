using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunDataBase : MonoBehaviour
{

    public List<GameObject> Guns = new List<GameObject>();


    public Sprite gunIsLocked;
    
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
            if (g.GetComponent<GunInfo>().item_id == id)
                return g;
        }
        return null;
    }

    public GunDataBase GetGunsByClass(WeaponClass weaponClass)
    {
        List<GameObject> list = new List<GameObject>();

        GunDataBase dataBase = new GunDataBase();
        foreach(var g in Guns)
        {
            if (g.GetComponent<GunInfo>().weaponClass == weaponClass)
                list.Add(g);
        }
        dataBase.Guns = list;
        return dataBase;
    }
}
