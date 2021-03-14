using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Igla : MonoBehaviour,IWeapon
{

    public float distance;

    public GameObject startTrigeringObj;

    public GameObject targetLockedObj;

    public float offset;

    public GameObject trigeredObject;

    public bool isTrigering;

    public bool isTargetLocked;

    public List<GameObject> showTrigeringObj = new List<GameObject>();

    public float trigeringTime;
    private float currTrigerTime;

    // Weap
    public Transform bulletStartPoint;

    public float Damage;

    public GameObject rocket;

    public float reloadTime;
    public float currReloadTime;

    public bool isReloading = false;


    public float BulletSpeed;

    public float BulletDestroyTime;

    public int ammo;

    public int maxAmmoInMagazine;

    public int currAmmo;

    public bool isFacingRight = true;

    public bool isOnPlayerHand = false;

    public float distanceBeforeFlyUp;


    private ParticleSystem particleSystem;


    public void Fire()
    {
        if (isTargetLocked)
        {
            
            if (currAmmo > 0)
            {
                var r = Instantiate(rocket, bulletStartPoint.position, Quaternion.Euler(0, 0, 0));
                r.GetComponent<IglaRocket>().target = trigeredObject;
                r.GetComponent<IglaRocket>().distanceBeforeFlyUp = distanceBeforeFlyUp;
                r.GetComponent<IglaRocket>().igla = this;
                --currAmmo;
                particleSystem.Play(true);
               

                if (currAmmo == 0 && ammo > 0)
                {
                    currReloadTime = reloadTime;
                    isReloading = !isReloading;
                }
            }
        }
    }

    public int GetAmmo()
    {
        return ammo;
    }

    public int GetCurrAmmo()
    {
        return currAmmo;
    }

    public float GetCurrReloadTime()
    {
        return currReloadTime;
    }

    public bool GetIsReloading()
    {
        return isReloading;
    }

    public float GetReloadTime()
    {
        return reloadTime;
    }

    public void Reload()
    {
        ammo += currAmmo;
        currAmmo = 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        rocket.GetComponent<IglaRocket>().damage = Damage;
        rocket.GetComponent<IglaRocket>().destroyTime = BulletDestroyTime;
        rocket.GetComponent<IglaRocket>().speed = BulletSpeed;

        particleSystem = GetComponent<ParticleSystem>();

    }

    // Update is called once per frame
    void Update()
    {
        if (isOnPlayerHand)
        {
            currReloadTime -= Time.deltaTime;

            if (isReloading && currReloadTime <= 0)
            {
                if (ammo > 0 && (ammo - maxAmmoInMagazine) > 0)
                {
                    ammo -= maxAmmoInMagazine;
                    currAmmo = maxAmmoInMagazine;
                }
                else
                {
                    if (ammo > 0)
                    {
                        currAmmo = ammo;
                        ammo = 0;
                    }
                }
                isReloading = !isReloading;
            }

            var pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) /*- transform.position*/;


            if ( /*Mathf.Abs(Mathf.Abs(pos.x) - Mathf.Abs(transform.position.x))*/
                Distance(pos.x, pos.y, pos.z, transform.position.x, transform.position.y, transform.position.z) < distance)
            {

                Camera.main.transform.position = pos;

            }
            else
            {
                var tmp = pos;
                tmp.x = transform.position.x + ((transform.position.x > pos.x) ? (-1) * distance : distance);
                var res = tmp;

                Camera.main.transform.position = res;
                Input.mousePosition.Set(res.x, res.y, res.z);
            }

            if (!isReloading && currAmmo > 0)
            {
                if (!isTrigering && !isTargetLocked)
                {
                    var obj_arr = (Tank[])FindObjectsOfType(typeof(Tank));
                    foreach (var obj in obj_arr)
                    {
                        if (Distance(obj.GetGameObject().transform.position.x, obj.GetGameObject().transform.position.y, obj.GetGameObject().transform.position.z,
                            transform.position.x, transform.position.y, transform.position.z) <= distance)
                        {
                            trigeredObject = obj.gameObject;
                            var gameObj = obj.GetGameObject();

                            var left_d = new Vector3(gameObj.transform.position.x - gameObj.GetComponent<BoxCollider2D>().bounds.size.x / 2 - offset,
                                gameObj.transform.position.y - gameObj.GetComponent<BoxCollider2D>().bounds.size.y / 2 - offset,
                                0);
                            var left_u = new Vector3(gameObj.transform.position.x - gameObj.GetComponent<BoxCollider2D>().bounds.size.x / 2 - offset,
                                gameObj.transform.position.y + gameObj.GetComponent<BoxCollider2D>().bounds.size.y / 2 + offset,
                                0);
                            var right_d = new Vector3(gameObj.transform.position.x + gameObj.GetComponent<BoxCollider2D>().bounds.size.x / 2 + offset,
                                gameObj.transform.position.y - gameObj.GetComponent<BoxCollider2D>().bounds.size.y / 2 - offset,
                                0);
                            var right_u = new Vector3(gameObj.transform.position.x + gameObj.GetComponent<BoxCollider2D>().bounds.size.x / 2 + offset,
                               gameObj.transform.position.y + gameObj.GetComponent<BoxCollider2D>().bounds.size.y / 2 + offset,
                               0);
                            showTrigeringObj.Add(Instantiate(startTrigeringObj, left_d, Quaternion.Euler(0, 0, 0)));
                            showTrigeringObj.Add(Instantiate(startTrigeringObj, left_u, Quaternion.Euler(0, 0, -90)));
                            showTrigeringObj.Add(Instantiate(startTrigeringObj, right_u, Quaternion.Euler(0, 0, 180)));
                            showTrigeringObj.Add(Instantiate(startTrigeringObj, right_d, Quaternion.Euler(0, 0, 90)));

                            isTrigering = true;
                            currTrigerTime = trigeringTime;
                        }
                    }
                }
                else
                {
                    

                    if (Distance(trigeredObject.transform.position.x, trigeredObject.transform.position.y, trigeredObject.transform.position.z,
                        transform.position.x, transform.position.y, transform.position.z) > distance && !isTargetLocked)
                    {
                        isTrigering = false;
                        foreach (var o in showTrigeringObj)
                        {
                            Destroy(o);
                        }
                       // currTrigerTime = trigeringTime;
                    }
                    else
                    {
                        currTrigerTime -= Time.deltaTime;

                        if (currTrigerTime <= 0)
                        {
                            if (!isTargetLocked)
                            {
                                foreach (var o in showTrigeringObj)
                                {
                                    Destroy(o);
                                }
                            }
                            isTargetLocked = true;

                            if (isTrigering)
                            {
                                var left = new Vector3(trigeredObject.transform.position.x - trigeredObject.GetComponent<BoxCollider2D>().bounds.size.x / 2 - offset,
                                    trigeredObject.transform.position.y,
                                    0);
                                var right = new Vector3(trigeredObject.transform.position.x + trigeredObject.GetComponent<BoxCollider2D>().bounds.size.x / 2 + offset,
                                    trigeredObject.transform.position.y,
                                    0);
                                var up = new Vector3(trigeredObject.transform.position.x,
                                    trigeredObject.transform.position.y + trigeredObject.GetComponent<BoxCollider2D>().bounds.size.y / 2 + offset,
                                    0);
                                var down = new Vector3(trigeredObject.transform.position.x,
                                   trigeredObject.transform.position.y - trigeredObject.GetComponent<BoxCollider2D>().bounds.size.y / 2 - offset,
                                   0);


                                var left_obj = Instantiate(targetLockedObj, left, Quaternion.Euler(0, 0, 0));
                                left_obj.transform.localScale = new Vector3(left_obj.transform.localScale.x,
                                    (trigeredObject.GetComponent<BoxCollider2D>().bounds.size.y + offset * 2) / left_obj.GetComponent<BoxCollider2D>().bounds.size.y);

                                var right_obj = Instantiate(targetLockedObj, right, Quaternion.Euler(0, 0, 0));
                                right_obj.transform.localScale = new Vector3(left_obj.transform.localScale.x,
                                    (trigeredObject.GetComponent<BoxCollider2D>().bounds.size.y + offset * 2) / right_obj.GetComponent<BoxCollider2D>().bounds.size.y);

                                var up_obj = Instantiate(targetLockedObj, up, Quaternion.Euler(0, 0, 90));
                                up_obj.transform.localScale = new Vector3(left_obj.transform.localScale.x,
                                    (trigeredObject.GetComponent<BoxCollider2D>().bounds.size.x + offset * 2) / up_obj.GetComponent<BoxCollider2D>().bounds.size.x);

                                var down_obj = Instantiate(targetLockedObj, down, Quaternion.Euler(0, 0, 90));
                                down_obj.transform.localScale = new Vector3(left_obj.transform.localScale.x,
                                    (trigeredObject.GetComponent<BoxCollider2D>().bounds.size.x + offset * 2) / down_obj.GetComponent<BoxCollider2D>().bounds.size.x);

                                showTrigeringObj.Add(left_obj);
                                showTrigeringObj.Add(right_obj);
                                showTrigeringObj.Add(up_obj);
                                showTrigeringObj.Add(down_obj);



                            }
                            isTrigering = false;
                        }
                    }
                }
            }
            
        }

    }

    public bool GetIsFacingRight()
    {
        return isFacingRight;
    }

    public void SetIsFacingRight(bool value)
    {
        isFacingRight = value;
    }

    public void SetIsOnPlayerHand(bool value)
    {
        isOnPlayerHand = value;
    }
    float Distance(float x1, float y1, float z1, float x2, float y2, float z2)
    {
        return Mathf.Sqrt(Mathf.Pow((x1 - x2), 2) + Mathf.Pow((y1 - y2), 2) + Mathf.Pow((z1 - z2), 2));
    }
}
