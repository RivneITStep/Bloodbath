using Assets.Scripts.Player;
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


    public void QuestComplited(QuestResult questResult)
    {
       

        var q = Instantiate(questPrevfab, questCanvas.transform);

        q.GetComponent<QuestDisplay>().Description.GetComponent<Text>().text = questResult.description;
        q.GetComponent<QuestDisplay>().Header.GetComponent<Text>().text = questResult.header;
        q.GetComponent<QuestDisplay>().Image.GetComponent<Image>().sprite = questResult.image;

        currQuest = q;
        questCanvas.SetActive(true);

    }




}
