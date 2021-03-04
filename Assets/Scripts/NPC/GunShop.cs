using Assets.Scripts;
using Assets.Scripts.Interfaces;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunShop : MonoBehaviour, IPageable
{
    public int maxCount = 19;

    public GameObject displayGameObject;

    public GameObject gameObjectMain;

    private GunDataBase gunDataBase;

    public Player player;

    public GameObject gunDataBaseObject;


    private List<Page> pages = new List<Page>();

    public int pageNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        //if (gameObjectMain != null)
        //    if (pages.Count > 0)
        //    {
        //        foreach (var e in pages[pageNumber].components)
        //        {
        //            e.SetActive(true);
        //        }
        //    }
    }

    void DeactiveElements(Page page)
    {

        foreach (var e in page.components)
        {
            e.SetActive(false);
        }

    }

    void ActivateCurrentPage()
    {
        if (gameObjectMain != null)
            if (pages.Count > 0)
            {
                foreach (var e in pages[pageNumber].components)
                {
                    e.SetActive(true);
                }
            }
    }

    public void OpenShop()
    {

        gunDataBase = gunDataBaseObject.GetComponent<GunDataBase>();
        
        gameObjectMain.transform.parent.parent.gameObject.SetActive(true); 
        gameObjectMain.transform.parent.gameObject.SetActive(true);
        gameObjectMain.SetActive(true);
        FillPages();
        ActivateCurrentPage();
    }


    public void FillPages()
    {

        Page currPage = new Page();
        currPage.emenentsMaxCount = 3;
        int i = 0;
        foreach (var g in gunDataBase.Guns)
        {
            GameObject newItem = Instantiate(displayGameObject, gameObjectMain.transform) as GameObject;
            newItem.SetActive(false);


            newItem.name = i.ToString();

            newItem.AddComponent(typeof(GunInfo));
            var info = newItem.GetComponent<GunInfo>();

            info.item_id = g.GetComponent<GunInfo>().item_id;
            info.DisplayName = g.GetComponent<GunInfo>().DisplayName;
            info.Rarity = g.GetComponent<GunInfo>().Rarity;
            info.weaponClass = g.GetComponent<GunInfo>().weaponClass;

            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempBtn = newItem.GetComponent<Button>();



            tempBtn.onClick.AddListener(delegate { SelectGun(info); });


            newItem.GetComponent<Image>().sprite = g.GetComponent<SpriteRenderer>().sprite;
            newItem.GetComponentInChildren<Text>().text = info.DisplayName;

            if (!player.unlockedGuns.Contains(info))
            {
                newItem.GetComponent<DisplayGunInfo>().gunIsLockedImage.SetActive(true);
                tempBtn.GetComponent<Image>().material = Resources.Load<Material>("Fonts & Materials/LiberationSans SDF");


                newItem.GetComponent<DisplayGunInfo>().gunIsLockedImage.GetComponent<Image>().sprite = gunDataBase.gunIsLocked;
            }


            if (currPage.emenentsMaxCount > currPage.components.Count)
            {
                currPage.AddElement(newItem);
            }
            else
            {
                pages.Add(currPage);
                currPage = new Page();
                currPage.emenentsMaxCount = 3;
                currPage.AddElement(newItem);
            }

            if (i == gunDataBase.Guns.Count - 1 && currPage.components.Count < currPage.emenentsMaxCount)
            {
                pages.Add(currPage);
            }
            
            
   
            i++;
        }

            
        
    }


    public void SelectGun(GunInfo info)
    {
        bool unlocked = player.unlockedGuns.Contains(info);

       
        if (unlocked) {

            var g = Instantiate(gunDataBase.GetById(info.item_id));
            if (player.itemInHand)
            {
                g.transform.position = player.transform.position;
            }
            else
            {

                player.item = g;
                player.itemInHand = true;
                if (g.GetComponent<GunInfo>().weaponClass == WeaponClass.SniperRiffle)
                    g.GetComponent<Sniper>().isOnPlayerHand = true;
            }


            CloseShop();
        }


    }

    public void CloseShop()
    {
        var g = gameObjectMain;
        gameObjectMain = null;
        var p = gameObject.transform.parent.gameObject;
        gameObject.transform.parent = null;
        p.transform.parent.gameObject.SetActive(false);
        //StopAllCoroutines();
        Destroy(g);
        Destroy(p);
        p = null;
    }

    public void Prev()
    {
        //if(pages.Count > 0)
       // {
            if(pageNumber -1 > -1)
            {
                DeactiveElements(pages[pageNumber]);
                pageNumber--;
                ActivateCurrentPage();
            }
       // }
    }

    public void Next()
    {
       // if (pages.Count > 0)
       // {
            if (pageNumber + 1 < pages.Count)
            {
                DeactiveElements(pages[pageNumber]);
                pageNumber++;
                ActivateCurrentPage();
            }
        //}
    }
}
