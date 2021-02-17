using Assets.Scripts;
using Assets.Scripts.Helpers;
using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    

    private Animator anim;

    bool isFacingRight = true;

    bool itemFacingRight = true;


    public List<GunInfo> unlockedGuns = new List<GunInfo>();

    public GameObject gunDataBaseObject;

    private GunDataBase gunDataBase;

   

   // public Camera camera;

    // For items

    public float Distance = 0.5f;
    public bool itemInHand = false;
    public RaycastHit2D hit;
    public Transform handPoint;
    public GameObject item;


    void Start()
    {
        this.anim = GetComponent<Animator>();

        gunDataBase = gunDataBaseObject.GetComponent<GunDataBase>();
        List<GunInfo> pistols = new List<GunInfo>();
        foreach(var g in (gunDataBase.GetGunsByClass(WeaponClass.Pistol).Guns))
        {
            pistols.Add(g.GetComponent<GunInfo>());
        }

        unlockedGuns.AddRange(pistols);
    }

    // Update is called once per frame
    void Update()
    {

        //camera.transform.position = transform.position;
        Camera.main.transform.position = transform.position;

        Physics2D.queriesStartInColliders = false;
        hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, Distance);
       
        if (Input.GetKey(KeyCode.D)) {

            if (!isFacingRight)
            {
                Flip();
                if (itemInHand && !itemFacingRight)
                {
                    FlipItem();
                }
            }
            anim.Play("Run");
            this.gameObject.transform.localPosition = 
                new Vector3(this.gameObject.transform.localPosition.x + 0.2f, this.gameObject.transform.localPosition.y, this.gameObject.transform.localPosition.z);       
        }
        else if (Input.GetKey(KeyCode.A))
        {
            if (isFacingRight)
            {
                Flip();
                if (itemInHand && itemFacingRight)
                {
                    FlipItem();
                }
            }
            anim.Play("Run");
            this.gameObject.transform.localPosition =
                new Vector3(this.gameObject.transform.localPosition.x - 0.2f, this.gameObject.transform.localPosition.y, this.gameObject.transform.localPosition.z);
        }
        else if (Input.GetKey(KeyCode.B))
        {
           
                anim.Play("Attack");
                       
                this.gameObject.transform.localPosition =
                new Vector3(this.gameObject.transform.localPosition.x + ((isFacingRight) ? 0.2f : -0.2f), this.gameObject.transform.localPosition.y, this.gameObject.transform.localPosition.z);
            
        }
        else
        {
            
            anim.Play("Rest");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {


            if (!itemInHand)
            {
               

                if (hit.collider != null && hit.collider.gameObject.tag == "Item")
                {
                    itemInHand = true;
                    item = hit.collider.gameObject;
                }
            }
            else
            {
                itemInHand = false;
            }
        }

        if (itemInHand)
        {
            item.transform.position = handPoint.position;
        }

        if (itemInHand)
        {
            // var mouse_pos = Input.mousePosition;
            //  mouse_pos.z = 5.23f; //The distance between the camera and object
            //var object_pos = Camera.main.WorldToScreenPoint(itemHelper.hit.collider.gameObject.transform.position);
            //  mouse_pos.x = mouse_pos.x - object_pos.x;
            // mouse_pos.y = mouse_pos.y - object_pos.y;
            //var angle = Mathf.Atan2(mouse_pos.y, mouse_pos.x) * Mathf.Rad2Deg;
            // itemHelper.hit.collider.gameObject.transform.rotation = Quaternion.Euler(0, 0, angle);



            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - item.transform.position;
            float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            
            item.transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);


        }

        if(Input.GetMouseButton(0) && itemInHand)
        {
            item.GetComponent<Weapon>().Fire();
        }

        if(hit.collider != null)
        {
            if(hit.collider.gameObject.tag == "NPC" && Input.GetKey(KeyCode.F))
            {
                var npc = hit.collider.gameObject;
                npc.GetComponent<INPCMenu>().OpenMenu();
            }
        }
        
    }

    private void Flip()
    {
       
        isFacingRight = !isFacingRight;
        
        Vector3 theScale = transform.localScale;
        
        theScale.x *= -1;
        
        transform.localScale = theScale;
    }

    private void FlipItem()
    {
        itemFacingRight = !itemFacingRight;

        Vector3 theScale = item.transform.localScale;
        theScale.x *= -1;
        item.transform.localScale = theScale;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * Distance);
    }
}
