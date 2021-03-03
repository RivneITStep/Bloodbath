using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDisplay : MonoBehaviour
{
    public GameObject Image;
    public GameObject Header;
    public GameObject Description;


    public float lifeTime;


    void Start()
    {
        Invoke("DestroyQuest", lifeTime);
    }


     void DestroyQuest()
    {
        Destroy(this.gameObject);
    }

}
