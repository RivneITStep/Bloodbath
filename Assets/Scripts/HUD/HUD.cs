using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{

    public GameObject statsObject;

    public GameObject questCanvas;

    public GameObject questPrevfab;

    public GameObject gunDataBaseObject;

    private GunDataBase gunDataBase;

    private GameObject currQuest;

    void Start()
    {
        gunDataBase = gunDataBaseObject.GetComponent<GunDataBase>();
    }

    void Update()
    {
        if(currQuest == null && questCanvas.active)
            questCanvas.SetActive(false);
    }


    public void QuestComplited(string item_id)
    {
        GameObject gun = gunDataBase.GetById(item_id);
        var info = gun.GetComponent<GunInfo>();

        var q = Instantiate(questPrevfab, questCanvas.transform);
        
        q.GetComponent<QuestDisplay>().Description.GetComponent<Text>().text = "Name: " + info.DisplayName + '\n' +
            "Rariry: " + info.Rarity.ToString();
        q.GetComponent<QuestDisplay>().Header.GetComponent<Text>().text = "Unlocked new gun";
        q.GetComponent<QuestDisplay>().Image.GetComponent<Image>().sprite = gun.GetComponent<SpriteRenderer>().sprite;

        currQuest = q;
        questCanvas.SetActive(true);

    }




}
