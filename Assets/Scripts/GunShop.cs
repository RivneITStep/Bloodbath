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


    private List<Page> pages = new List<Page>();

    public int pageNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        gunDataBase = GetComponentInChildren<GunDataBase>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if(gameObjectMain != null)
        if (pages.Count > 0) { 
            foreach(var e in pages[pageNumber].components)
            {
                e.SetActive(true);
            }
        }
    }

    void DeactiveElements(Page page)
    {

        foreach (var e in page.components)
        {
            e.SetActive(false);
        }

    }

    public void OpenShop()
    {
        gameObjectMain.transform.parent.parent.gameObject.SetActive(true); 
        gameObjectMain.transform.parent.gameObject.SetActive(true);
        gameObjectMain.SetActive(true);
        FillPages();
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

            newItem.AddComponent(typeof(ItemInfo));
            var info = newItem.GetComponent<ItemInfo>();

            info.item_id = g.GetComponent<ItemInfo>().item_id;
            info.DisplayName = g.GetComponent<ItemInfo>().DisplayName;
            info.Rarity = g.GetComponent<ItemInfo>().Rarity;

            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempBtn = newItem.GetComponent<Button>();



            tempBtn.onClick.AddListener(delegate { BuyGun(info); });


            newItem.GetComponent<Image>().sprite = g.GetComponent<SpriteRenderer>().sprite;
            newItem.GetComponentInChildren<Text>().text = info.DisplayName;


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
            
            
            
   
            i++;
        }

            
        
    }


    public void BuyGun(ItemInfo info)
    {


        var p = gameObjectMain;
        gameObjectMain = null;
        Instantiate(gunDataBase.GetById(info.item_id)).transform.position = player.transform.position;
     
        p.transform.parent.gameObject.SetActive(false);
        //StopAllCoroutines();
        Destroy(p);
        p = null;
    }

    public void Prev()
    {
        if(pages.Count > 0)
        {
            if(pageNumber -1 > -1)
            {
                DeactiveElements(pages[pageNumber]);
                pageNumber--;
            }
        }
    }

    public void Next()
    {
        if (pages.Count > 0)
        {
            if (pageNumber + 1 < pages.Count)
            {
                DeactiveElements(pages[pageNumber]);
                pageNumber++;
            }
        }
    }
}
