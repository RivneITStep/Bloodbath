using Assets.Scripts.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{


    public DataBase dataBase;

    public List<InventoryItem> items = new List<InventoryItem>();


    public GameObject gameObjectDisplay;

    public GameObject gameObjectMain;

    public int maxCount;

    public Camera cam;

    public EventSystem es;

    public int currentID;
    public InventoryItem currInvItem;

    public RectTransform movingObject;
    public Vector3 offset;


    public void Start()
    {
        if(items.Count == 0)
        {
            AddGraphics();
        }

        for (int i =0; i < maxCount;i++)
        {
            AddItem(i, dataBase.Items[Random.Range(0, dataBase.Items.Count)], Random.Range(1, 50));
            UpdateInventory();
        }
    }

    public void Update()
    {
        if(currentID != -1)
        {
            MoveObject();
        }
    }

    public void AddItem(int id, Item item, int count)
    {
        items[id].ID = item.Item_ID;
        items[id].Count = count;
        items[id].GameObject.GetComponent<Image>().sprite = item.Item_Sprite;

        if(count > 1 && item.Item_ID != "")
        {
            items[id].GameObject.GetComponentInChildren<Text>().text = count.ToString();
        }
        else
        {
            items[id].GameObject.GetComponentInChildren<Text>().text = "";
        }
    }

    public void AddInventoryItem(int id, InventoryItem Invitem)
    {
        items[id].ID = Invitem.ID;
        items[id].Count = Invitem.Count;
        items[id].GameObject.GetComponent<Image>().sprite = dataBase.GetItemById(Invitem.ID).Item_Sprite;

        if (Invitem.Count > 1 && Invitem.ID != "")
        {
            items[id].GameObject.GetComponentInChildren<Text>().text = Invitem.Count.ToString();
        }
        else
        {
            items[id].GameObject.GetComponentInChildren<Text>().text = "";
        }
    }


    public void AddGraphics()
    {
        for(int i = 0; i < maxCount; i++)
        {
            GameObject newItem = Instantiate(gameObjectDisplay, gameObjectMain.transform) as GameObject;

            newItem.name = i.ToString();

            InventoryItem inventoryItem = new InventoryItem();
            inventoryItem.GameObject = newItem;
            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1,1,1);

            Button tempBtn = newItem.GetComponent<Button>();

            tempBtn.onClick.AddListener(delegate { SelectObject(); });

            items.Add(inventoryItem);
        }
    }

    public void SelectObject()
    {
        if(currentID == -1)
        {
            currentID = int.Parse(es.currentSelectedGameObject.name);
            currInvItem = CopyInventoryItem(items[currentID]);
            movingObject.gameObject.SetActive(true);
            movingObject.GetComponent<Image>().sprite = dataBase.GetItemById(currInvItem.ID).Item_Sprite;

            AddItem(currentID, dataBase.Items[0], 0);
        }
        else
        {
            AddInventoryItem(currentID,items[int.Parse(es.currentSelectedGameObject.name)]);

            AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currInvItem);
            currentID = -1;

            movingObject.gameObject.SetActive(false);
        }
    }

    public void UpdateInventory()
    {
        for (int i  = 0; i < maxCount;i++)
        {
            if(items[i].ID != "" && items[i].Count > 1)
            {
                items[i].GameObject.GetComponentInChildren<Text>().text = items[i].Count.ToString();
            }
            else
            {
                items[i].GameObject.GetComponentInChildren<Text>().text = "";
            }
          //  items[i].GameObject.GetComponent<Image>().sprite = dataBase.GetItemById(items[i].ID).Item_Sprite;
        }
    }

    public void MoveObject()
    {
        Vector3 pos = Input.mousePosition+ offset;
        pos.z = gameObjectMain.GetComponent<RectTransform>().position.z;
        movingObject.position = cam.ScreenToWorldPoint(pos);
    }

    public InventoryItem CopyInventoryItem(InventoryItem item)
    {
        InventoryItem newI = new InventoryItem();
        newI.ID = item.ID;
        newI.GameObject = item.GameObject;
        newI.Count = item.Count;

        return newI;
    }
}
