using Assets.Scripts;
using Assets.Scripts.Helpers;
using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    

    private Animator anim;

    bool isFacingRight = true;

   


    public List<GunInfo> unlockedGuns = new List<GunInfo>();

    public GameObject gunDataBaseObject;

    public GunDataBase gunDataBase;


    public List<Quest> quests = new List<Quest>();
   

   // public Camera camera;

    // For items

    public float Distance = 0.5f;
    public bool itemInHand = false;
    public RaycastHit2D hit;
    public Transform handPoint;
    public GameObject item;

    // HUD
    public GameObject hudObject;
    private HUD hud;
    private Stats stats;
    private Ammo ammo;
    public GameObject reloadBarObject;




    private int healthPoints = 100;
    private int armor = 5;


    void Start()
    {
        this.anim = GetComponent<Animator>();

        hud = hudObject.GetComponent<HUD>();

        stats = hud.statsObject.GetComponent<Stats>();

        ammo = hud.ammoObject.GetComponent<Ammo>();

        stats.SetHP(healthPoints);
        stats.SetArmor(armor);


        gunDataBase = gunDataBaseObject.GetComponent<GunDataBase>();
        List<GunInfo> pistols = new List<GunInfo>();
        foreach(var g in (gunDataBase.Guns))
        {
            pistols.Add(g.GetComponent<GunInfo>());
        }

        unlockedGuns.AddRange(pistols);

        Quest newQ = new Quest();
        QuestResult questResult = new QuestResult();

        newQ.isActive = true;
        newQ.check = QuestFunctionsList.ScarLUnlockCheck;
        newQ.doresult = QuestFunctionsList.ScalLUnclockDoResult;
        newQ.quest_id = "unlock_scarL";

        questResult.header = "Unlocked new gun";
        questResult.description = "Name: " + gunDataBase.GetById("Assault_scarl").GetComponent<GunInfo>().DisplayName + '\n'
            + "Rarity: " + gunDataBase.GetById("Assault_scarl").GetComponent<GunInfo>().Rarity.ToString();
        questResult.image = gunDataBase.GetById("Assault_scarl").GetComponent<SpriteRenderer>().sprite;
        
        
        newQ.result = questResult;

        quests.Add(newQ);


        foreach(var q in quests)
        {
            q.player = this;
            
        }
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
                if (itemInHand && !item.GetComponent<IWeapon>().GetIsFacingRight())
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
                if (itemInHand && item.GetComponent<IWeapon>().GetIsFacingRight())
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
            
         //   anim.Play("Rest");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {


            if (!itemInHand)
            {
               

                if (hit.collider != null && hit.collider.gameObject.tag == "Weapon")
                {
                    itemInHand = true;
                    item = hit.collider.gameObject;
                    if (isFacingRight && !item.GetComponent<IWeapon>().GetIsFacingRight())
                    {
                        FlipItem();
                    }

                    if(item.GetComponent<GunInfo>().weaponClass == WeaponClass.SniperRiffle)
                    {
                        item.GetComponent<Sniper>().isOnPlayerHand = true;
                    }
                }
            }
            else
            {
                if(item.tag == "Weapon")
                    hud.ammoObject.SetActive(false);
                itemInHand = false;
                if (item.GetComponent<GunInfo>().weaponClass == WeaponClass.SniperRiffle)
                {
                    item.GetComponent<Sniper>().isOnPlayerHand = false;
                }
                item = null;
                reloadBarObject.SetActive(false);
            }
        }

        if (item != null && item.GetComponent<IWeapon>() != null)
        {
            hud.ammoObject.SetActive(true);

            var weap = item.GetComponent<IWeapon>();
            var info = item.GetComponent<GunInfo>();
            ammo.textObject.GetComponent<Text>().text = weap.GetCurrAmmo().ToString() + '/' + weap.GetAmmo().ToString();
            switch (info.weaponClass) {
                case WeaponClass.Pistol:
                    ammo.bulletImageObject.GetComponent<Image>().sprite = ammo.BulletSprite9mm;
                    break;
            }


            if (weap.GetIsReloading() && !reloadBarObject.active)
            {
                reloadBarObject.SetActive(true);     
            }
            if (weap.GetIsReloading())
            {
                Vector3 scale = reloadBarObject.transform.localScale;
                scale.x = ((weap.GetCurrReloadTime() * 100 / weap.GetReloadTime()) * 2) / 100;
                reloadBarObject.transform.localScale = scale;
            }
            if(!weap.GetIsReloading() && reloadBarObject.active)
                reloadBarObject.SetActive(false);
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



            //Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - item.transform.position;
           // float rotateZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;

            
           // item.transform.rotation = Quaternion.Euler(0f, 0f, rotateZ);


        }

        if(Input.GetMouseButton(0) && itemInHand)
        {
            item.GetComponent<IWeapon>().Fire();
        }

        if(hit.collider != null)
        {
            if(hit.collider.gameObject.tag == "NPC" && Input.GetKey(KeyCode.F))
            {
                var npc = hit.collider.gameObject;
                npc.GetComponent<INPCMenu>().OpenMenu();
                
            }
        }


        // Quest check

        foreach(var q in quests.FindAll(x=>x.isActive))
        {
            var res = q.CheckComplite();
            if(res != null)
            {
                hud.QuestComplited(res);
            }
        }

        
    }

    

    private void Flip()
    {
       
        isFacingRight = !isFacingRight;

        //Vector3 theScale = transform.localScale;

        //theScale.x *= -1;

        //transform.localScale = theScale;
        transform.Rotate(0f,180f,0f);
    }

    private void FlipItem()
    {
        item.GetComponent<IWeapon>().SetIsFacingRight(!item.GetComponent<IWeapon>().GetIsFacingRight());

        //Vector3 theScale = item.transform.localScale;
        //theScale.x *= -1;
        //item.transform.localScale = theScale;
        item.transform.Rotate(0f, 180f, 0f);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.right * transform.localScale.x * Distance);
    }
}
